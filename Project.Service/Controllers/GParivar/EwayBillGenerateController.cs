using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class EwayBillGenerateController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/EwayBillGenerate")]
        public HttpResponseMessage GetDetails(ListsofEwayBillGenerate ula)
        {
            Common cm = new Common();
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;
            var key = string.Empty;
            var data = string.Empty;
            decimal distance = 0;
            if (ula.userid != 0 && ula.slno != 0)
            {
                try
                {
                    List<EwayBillGenerates> head = new List<EwayBillGenerates>();
                    List<EwayBillGenerateItemList> child = new List<EwayBillGenerateItemList>();
                    List<ewaybill> ewaybill = new List<ewaybill>();
                    List<ewaybills> result = new List<ewaybills>();
                    // int active = 0;
                     key = cm.GetEwayTokanno();
                   // key = "12644";


                    if (key != "")
                    {
                        var dr3 = g2.return_dt("EwayBillDatabySlno " + ula.slno+","+ula.type);


                        if (dr3.Rows.Count > 0)
                        {
                            if (!String.IsNullOrEmpty(dr3.Rows[0]["brpin"].ToString()) && !String.IsNullOrEmpty(dr3.Rows[0]["topin"].ToString()))
                            {
                              
                                var dr10 = g2.return_dt("pintopindistancecheck '" + dr3.Rows[0]["brpin"].ToString() + "','" + dr3.Rows[0]["topin"].ToString() + "'");

                                if (dr10.Rows.Count > 0)
                                {
                                    distance = Convert.ToDecimal(dr10.Rows[0]["Distance"]);
                                }
                                else
                                {
                                    var baseurl = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + dr3.Rows[0]["brpin"].ToString() + "+IN&destinations=" + dr3.Rows[0]["topin"].ToString() + " +IN&mode=driving&language=en-EN&sensor=false&key=AIzaSyCuYEQogqF3cTj_f8oj-eM3YabPaF57js4";

                                    RemoteStatus res;
                                    using (var remoteClient = new RemoteClient())
                                    {
                                        res = remoteClient.GetAsync(url: baseurl).Result;
                                    }

                                    var json1 = JObject.Parse(res.Response);


                                   // dynamic _output = JsonConvert.DeserializeObject(res.Response).ToString();
                                    if (res.StatusCode == 200)

                                    {
                                       // string jjj = json1.SelectToken("rows[0].elements[0].distance.text").ToString();
                                      //  string jjjw = json1.SelectToken("rows[0].elements[0].distance.value").ToString();
                                        string[] tokens = json1.SelectToken("rows[0].elements[0].distance.text").ToString().Split(' ');
                                        distance = Convert.ToDecimal(tokens[0]);

                                        var dr9 = g2.return_dt("pintopindistanceInsert '" + dr3.Rows[0]["brpin"].ToString() +"','" + dr3.Rows[0]["topin"].ToString() + "','" + distance + "','km'");
                                    }

                                }

                               

                                if (!String.IsNullOrEmpty(dr3.Rows[0]["transporterid"].ToString()) && !String.IsNullOrEmpty(dr3.Rows[0]["transportername"].ToString()) && distance!=0)
                                {
                                    var dr4 = g2.return_dr("EwayBillDataChildbySlno " + ula.slno+","+ula.type);
                                    while (dr4.Read())
                                    {
                                        child.Add(new EwayBillGenerateItemList
                                        {
                                            // for Production //
                                            productName = dr4["ProductCode1"].ToString(),
                                            productDesc = dr4["ProductCode"].ToString(),
                                            hsnCode = dr4["hsn"].ToString(),
                                            quantity = dr4["itemqty"].ToString(),
                                            qtyUnit = dr4["unitnm"].ToString().ToUpper(),
                                            cgstRate = dr4["cgstrate"].ToString(),
                                            sgstRate = dr4["sgstrate"].ToString(),
                                            igstRate = dr4["igstrate"].ToString(),
                                            cessRate = dr4["cessrate"].ToString(),
                                            cessAdvol = dr4["cessAdvol"].ToString(),
                                            taxableAmount = dr4["pretaxamt"].ToString(),


                                            // for Testing //
                                            //productName = dr4["ProductCode1"].ToString(),
                                            //productDesc = dr4["ProductCode"].ToString(),
                                            //hsnCode = dr4["hsn"].ToString(),
                                            //quantity = dr4["itemqty"].ToString(),
                                            //qtyUnit = dr4["unitnm"].ToString(),
                                            //cgstRate = "0",
                                            //sgstRate = "0",
                                            //igstRate = "18",
                                            //cessRate = dr4["cessrate"].ToString(),
                                            //cessAdvol = dr4["cessAdvol"].ToString(),
                                            //taxableAmount = dr4["pretaxamt"].ToString(),
                                        });
                                    }


                                    for (int i = 0; i < dr3.Rows.Count; i++)

                                    {
                                        head.Add(new EwayBillGenerates
                                        {

                                            // for Production //
                                            supplyType = "O",
                                            subSupplyType = dr3.Rows[i]["subSupplyType"].ToString(),
                                            docType = dr3.Rows[i]["doctype"].ToString(),
                                            // docType = "CHL",
                                            subSupplyDesc = dr3.Rows[i]["docdec"].ToString(),
                                            docNo = dr3.Rows[i]["invoiceno"].ToString(),
                                            docDate = dr3.Rows[i]["invoicedate"].ToString(),
                                            fromGstin = dr3.Rows[i]["GSTNo"].ToString(),
                                            fromTrdName = dr3.Rows[i]["trdnm"].ToString(),
                                            fromAddr1 = dr3.Rows[i]["add1"].ToString().Replace(',', ' '),
                                            fromAddr2 = dr3.Rows[i]["add2"].ToString().Replace(',', ' '),
                                            fromPlace = dr3.Rows[i]["fromplace"].ToString(),
                                            fromPincode = dr3.Rows[i]["brpin"].ToString(),
                                            actFromStateCode = dr3.Rows[i]["frmstatecode"].ToString(),
                                            fromStateCode = dr3.Rows[i]["frmstatecode"].ToString(),
                                            toGstin = dr3.Rows[i]["togst"].ToString(),
                                            toTrdName = dr3.Rows[i]["name"].ToString(),
                                            toAddr1 = dr3.Rows[i]["add11"].ToString().Replace(',', ' '),
                                            toAddr2 = dr3.Rows[i]["add22"].ToString().Replace(',', ' '),
                                            toPlace = dr3.Rows[i]["toplace"].ToString(),
                                            toPincode = dr3.Rows[i]["topin"].ToString(),
                                            actToStateCode = dr3.Rows[i]["tostatecode"].ToString(),
                                            toStateCode = dr3.Rows[i]["tostatecode"].ToString(),
                                            totalValue = dr3.Rows[i]["pretaxamt"].ToString(),
                                            cgstValue = dr3.Rows[i]["cgst"].ToString(),
                                            sgstValue = dr3.Rows[i]["sgst"].ToString(),
                                            igstValue = dr3.Rows[i]["igst"].ToString(),
                                            cessValue = dr3.Rows[i]["cess"].ToString(),
                                            totInvValue = dr3.Rows[i]["invoiceval"].ToString(),
                                            transporterId = dr3.Rows[i]["transporterid"].ToString(),
                                            transporterName = dr3.Rows[i]["transportername"].ToString(),
                                            transDocNo = "",
                                            transMode = "",
                                            transDistance = distance.ToString(),
                                            transDocDate = "",
                                            vehicleNo = "",
                                            vehicleType = "",
                                            TransactionType = "1",
                                            itemList = child


                                            // for Testing //
                                            //supplyType = "O",
                                            //subSupplyType = "1",
                                            //docType = "INV",
                                            //docNo = cm.GenerateRandomNo(3) + cm.GenerateRandomString(7).ToLower(),
                                            //docDate = dr3["invoicedate"].ToString(),
                                            //fromGstin = "05AAACG2115R1ZN",
                                            //fromTrdName = "welton",
                                            //fromAddr1 = "2ND CROSS NO 59  19  A",
                                            //fromAddr2 = "GROUND FLOOR OSBORNE ROAD",
                                            //fromPlace = "FRAZER TOWN",
                                            //fromPincode = "560042",
                                            //actFromStateCode = "29",
                                            //fromStateCode = "29",
                                            //toGstin = "05AAACG2140A1ZL",
                                            //toTrdName = "sthuthya",
                                            //toAddr1 = "Shree Nilaya",
                                            //toAddr2 = "Dasarahosahalli",
                                            //toPlace = "Beml Nagar",
                                            //toPincode = "500003",
                                            //actToStateCode = "36",
                                            //toStateCode = "36",
                                            //totalValue = dr3["pretaxamt"].ToString(),
                                            //cgstValue = "0",
                                            //sgstValue = "0",
                                            //igstValue = (Convert.ToDouble(dr3["igst"]) + Convert.ToDouble(dr3["cgst"]) + Convert.ToDouble(dr3["sgst"])).ToString(),
                                            //cessValue = dr3["cess"].ToString(),
                                            //totInvValue = dr3["invoiceval"].ToString(),
                                            //transporterId = "",
                                            //transporterName = "",
                                            //transDocNo = "",
                                            //transMode = "1",
                                            //transDistance = "25",
                                            //transDocDate = "",
                                            //vehicleNo = "PVC1234",
                                            //vehicleType = "R",
                                            //TransactionType = "1",
                                            //itemList = child
                                        });
                                    }


                                    var dr5 = g2.return_dt("EwayBillGenerateHeader " + ula.slno + ","+ula.type);
                                    if (dr5.Rows.Count > 0)
                                    {

                                        string requtid = string.Empty;
                                        requtid = dr5.Rows[0]["locnm"].ToString().Substring(0, 3) + cm.GenerateRandomNo(10);

                                        var baseurl = ConfigurationManager.AppSettings["Gold.Eway.API"] + "GENEWAYBILL";
                                        System.Net.HttpWebRequest request1 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(baseurl);

                                        // for production //
                                        request1.Method = "POST";
                                        request1.ContentType = "application/json";
                                        request1.Headers.Add("Username", dr5.Rows[0]["userid"].ToString());
                                        request1.Headers.Add("Password", dr5.Rows[0]["password"].ToString());
                                        request1.Headers.Add("gstin", dr5.Rows[0]["GSTNo"].ToString());
                                        request1.Headers.Add("requestid", requtid);
                                        request1.Headers.Add("Authorization", "Bearer " + key);

                                        // for testing //
                                        //request1.Method = "POST";
                                        //request1.ContentType = "application/json";
                                        //request1.Headers.Add("Username", "05AAACG2115R1ZN");
                                        //request1.Headers.Add("Password", "abc123@@");
                                        //request1.Headers.Add("gstin", "05AAACG2115R1ZN");
                                        //request1.Headers.Add("requestid", requtid);
                                        //request1.Headers.Add("Authorization", "Bearer " + key);

                                        //Place the serialized content of the object to be posted into the request stream
                                        using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                                        {
                                            var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(head).TrimEnd(']').TrimStart('[');
                                            streamWriter.Write(json);
                                            streamWriter.Flush();
                                            streamWriter.Close();
                                            var dr99 = g2.return_dr("InsertEwayInput " + ula.slno + ",'" + json.ToString() + "'");
                                        }


                                        System.Net.HttpWebResponse response1 = (System.Net.HttpWebResponse)request1.GetResponse();




                                        if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                                        {

                                            dynamic _output = JsonConvert.DeserializeObject((new StreamReader(response1.GetResponseStream())).ReadToEnd());

                                            if (_output.success = true && _output.message == "E-Way Bill is generated successfully")
                                            {


                                                var dr6 = g2.return_dr("EwayBillupdatebyAPI " + ula.slno + ",'"
                                    + _output.result.ewayBillNo + "'," + ula.userid+","+ula.type

                                    );
                                                if (dr6.HasRows)
                                                {
                                                    ewaybill.Add(new ewaybill
                                                    {
                                                        ewaybillno = _output.result.ewayBillNo,
                                                    });

                                                    g2.close_connection();
                                                    result.Add(new ewaybills
                                                    {
                                                        result = true,
                                                        message = string.Empty,
                                                        servertime = DateTime.Now.ToString(),
                                                        data = ewaybill,
                                                    });
                                                    data = JsonConvert.SerializeObject(result, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                                    HttpResponseMessage response8 = Request.CreateResponse(HttpStatusCode.OK);

                                                    response8.Content = new StringContent(data, Encoding.UTF8, "application/json");

                                                    return response8;
                                                }
                                                else
                                                {
                                                    HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                                    response9.Content = new StringContent(cm.StatusTime(false, "Oops! Ewaybill not updated in database!!!!!!!!"), Encoding.UTF8, "application/json");

                                                    return response9;

                                                }
                                            }
                                            else
                                            {
                                                HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                                response9.Content = new StringContent(cm.StatusTime(false, _output.message.ToString()), Encoding.UTF8, "application/json");

                                                return response9;

                                            }
                                        }
                                        else
                                        {
                                            HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                            response9.Content = new StringContent(cm.StatusTime(false, "Oops! Not a sucess Result from API!!!!!!!!"), Encoding.UTF8, "application/json");

                                            return response9;

                                        }


                                    }
                                    else
                                    {
                                        HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                        response9.Content = new StringContent(cm.StatusTime(false, "Oops! No Proper Header or Incorrect Userid and Password!!!!!!!!"), Encoding.UTF8, "application/json");

                                        return response9;

                                    }
                                }
                                else
                                {
                                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Atleast Transporter Id or Transporter GSTNo,Transporter Name Required and distance, Thats missing!!!!!!!!"), Encoding.UTF8, "application/json");

                                    return response;
                                }
                            }
                            else
                            {
                                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "Oops! From Pincode or To Pincode or both missing !!!!!!!!"), Encoding.UTF8, "application/json");

                                return response;
                            }
                        }

                        else
                        {
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Oops! No Data Available in database!!!!!!!!"), Encoding.UTF8, "application/json");

                            return response;
                        }




                    }
                    else
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Key not generated!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {

                    if (ex.Message == "The remote server returned an error: (401) Unauthorized.")
                    {
                        var dr5 = g2.return_dt("EwayBillKeyInactive");
                      
                    }

                    if(distance==0)
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Distance not available on Google portal, please update manually!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message.ToString()), Encoding.UTF8, "application/json");

                        return response;
                    }
                 
                }
            }
            else
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Not a valid invoice or user."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}