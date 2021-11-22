using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;


namespace Project.Service.Controllers
{
    public class EWayGeneratebyInvoiceController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/EWayGeneratebyInvoice")]
        public HttpResponseMessage GetDetails(ListsofEwayGeneratebyEInvoice ula)
        {
            Common cm = new Common();
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;
            var key = new EinvoiceKey();
            var data = string.Empty;
            decimal distance = 0;
            if (ula.userid != 0 && ula.slno != 0)
            {
                try
                {
                    List<Root> head = new List<Root>();
                    List<EwayGeneratebyEInvoice> ewaybill = new List<EwayGeneratebyEInvoice>();
                    List<EwayGeneratebyEInvoices> result = new List<EwayGeneratebyEInvoices>();
                    

                    var dr3 = g2.return_dt("EInvoiceSubDatabySlno " + ula.slno);


                    if (dr3.Rows.Count > 0)
                    {
                        string output = EWayGeneratebyInvoicevalidation(dr3);

                        if (output == "")
                        {

                            //   key = cm.GetEInvoiceTokanno(dr3.Rows[0]["GSTUser"].ToString(), dr3.Rows[0]["GSTPassword"].ToString(), dr3.Rows[0]["ClientID"].ToString(), dr3.Rows[0]["ClientSecret"].ToString());
                            key = cm.GetEInvoiceTokanno("goldluein", "111@Abc@", "AACCG09TXPMK3F7", "Ppd4J8XLl2zFeTshMBa1");

                            if (key.AuthToken != "" && key.Sek != "")
                            {

                                if (!String.IsNullOrEmpty(dr3.Rows[0]["SellerDtlsPin"].ToString()) && !String.IsNullOrEmpty(dr3.Rows[0]["ExpShipPin"].ToString()))
                                {

                                    var dr10 = g2.return_dt("pintopindistancecheck '" + dr3.Rows[0]["SellerDtlsPin"].ToString() + "','" + dr3.Rows[0]["ExpShipPin"].ToString() + "'");

                                    if (dr10.Rows.Count > 0)
                                    {
                                        distance = Convert.ToDecimal(dr10.Rows[0]["Distance"]);
                                    }
                                    else
                                    {
                                        var baseurl = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + dr3.Rows[0]["SellerDtlsPin"].ToString() + "+IN&destinations=" + dr3.Rows[0]["ExpShipPin"].ToString() + " +IN&mode=driving&language=en-EN&sensor=false&key=AIzaSyCuYEQogqF3cTj_f8oj-eM3YabPaF57js4";

                                        RemoteStatus res;
                                        using (var remoteClient = new RemoteClient())
                                        {
                                            res = remoteClient.GetAsync(url: baseurl).Result;
                                        }

                                        var json1 = JObject.Parse(res.Response);



                                        if (res.StatusCode == 200)

                                        {

                                            string[] tokens = json1.SelectToken("rows[0].elements[0].distance.text").ToString().Split(' ');
                                            distance = Convert.ToDecimal(tokens[0]);

                                            var dr9 = g2.return_dt("pintopindistanceInsert '" + dr3.Rows[0]["SellerDtlsPin"].ToString() + "','" + dr3.Rows[0]["ExpShipPin"].ToString() + "','" + distance + "','km'");
                                        }

                                    }





                                    if (dr3.Rows.Count > 0)
                                    {

                                        var data1 = new Root
                                        {
                                            Irn = dr3.Rows[0]["Irn"].ToString(),
                                            Distance = Convert.ToInt32(distance),
                                            TransMode = dr3.Rows[0]["TransMode"].ToString(),
                                            TransId = dr3.Rows[0]["TransId"].ToString(),
                                            TransName = dr3.Rows[0]["TransName"].ToString(),
                                            TransDocNo = dr3.Rows[0]["TransDocNo"].ToString(),
                                            TransDocDt = dr3.Rows[0]["TransDocDt"].ToString(),
                                            VehNo = dr3.Rows[0]["VehNo"].ToString(),
                                            VehType = dr3.Rows[0]["VehType"].ToString(),
                                            ExpShipDtls = new ExpShipDtls
                                            {
                                                Addr1 = dr3.Rows[0]["ExpShipAddr1"].ToString(),
                                                Addr2 = dr3.Rows[0]["ExpShipAddr2"].ToString(),
                                                Loc = dr3.Rows[0]["ExpShipLoc"].ToString(),
                                                Pin = Convert.ToInt32(dr3.Rows[0]["ExpShipPin"].ToString()),
                                                Stcd = dr3.Rows[0]["ExpShipStcd"].ToString(),
                                            }
                                        };


                                        var baseurl = ConfigurationManager.AppSettings["Gold.Invoice.API"] + "eiewb/v1.03/ewaybill";
                                        System.Net.HttpWebRequest request1 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(baseurl);

                                        //// for production //
                                        //request1.Method = "POST";
                                        //request1.ContentType = "application/json";
                                        //request1.Headers.Add("client_id", dr3.Rows[0]["ClientID"].ToString());
                                        //request1.Headers.Add("client_secret", dr3.Rows[0]["ClientSecret"].ToString());
                                        //request1.Headers.Add("gstin", dr3.Rows[0]["SellerDtlsGstin"].ToString());
                                        //request1.Headers.Add("user_name", dr3.Rows[0]["GSTUser"].ToString());
                                        //request1.Headers.Add("AuthToken", key.AuthToken);

                                        // for testing //
                                        request1.Method = "POST";
                                        request1.ContentType = "application/json";
                                        request1.Headers.Add("client_id", "AACCG09TXPMK3F7");
                                        request1.Headers.Add("client_secret", "Ppd4J8XLl2zFeTshMBa1");
                                        request1.Headers.Add("gstin", "09AACCG9397F1Z4");
                                        request1.Headers.Add("user_name", "goldluein");
                                        request1.Headers.Add("AuthToken", key.AuthToken);


                                        //Encryt the data


                                        var json1 = new JavaScriptSerializer().Serialize(data1);

                                        var datatocall = cm.EncryptBySymmetricKey(json1, key.Sek);


                                        RootInvoice root = new RootInvoice
                                        {
                                            Data = datatocall
                                        };

                                        //Place the serialized content of the object to be posted into the request stream
                                        using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                                        {
                                            var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(root);
                                            streamWriter.Write(json);
                                            streamWriter.Flush();
                                            streamWriter.Close();
                                        }


                                        //  System.Net.HttpWebResponse response1 = null;
                                        System.Net.HttpWebResponse response1 = (System.Net.HttpWebResponse)request1.GetResponse();




                                        if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                                        {

                                            dynamic _output = JsonConvert.DeserializeObject((new StreamReader(response1.GetResponseStream())).ReadToEnd());

                                            if (Convert.ToInt32(_output.Status) == 1)
                                            {


                                                var jsondata = cm.DecryptBySymmetricKey1(_output.Data.ToString(), key.Sek);

                                                //  dynamic _output1 = JsonConvert.DeserializeObject((new StreamReader(jsondata)).ReadToEnd());

                                                ResponseEInvoiceEway flight = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseEInvoiceEway>(jsondata);

                                                var sqlstring = ula.slno + ",'"
                                    + flight.EwbNo.ToString() + "','"
                                    + flight.EwbDt.ToString() + "','"
                                    + flight.EwbValidTill.ToString() + "'";
                                                // + flight.Remarks.ToString()+ "'";
                                                //+ flight.Status.ToString() + "','"
                                                //+ flight.EwbNo.ToString() + "','"
                                                //+ flight.EwbDt.ToString() + "','";
                                                //+ flight.EwbValidTill.ToString() + "','"
                                                // +flight.Remarks.ToString() + "'";
                                                //+ ula.userid.ToString();

                                                var dr6 = g2.return_dt("EwaybillInvoiceupdatebyAPI " + sqlstring

                                    );
                                                if (dr6.Rows.Count > 0)
                                                {
                                                    ewaybill.Add(new EwayGeneratebyEInvoice
                                                    {
                                                        ewaybillno = flight.EwbNo.ToString(),
                                                    });

                                                    g2.close_connection();
                                                    result.Add(new EwayGeneratebyEInvoices
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
                                               //   var dr9 = g2.return_dt("EInvoiceinvalidtokan '" +  dr3.Rows[0]["GSTUser"].ToString()+"'");
                                                var dr9 = g2.return_dt("EInvoiceinvalidtokan " + "goldluein");
                                                HttpResponseMessage response9 = request.CreateResponse(HttpStatusCode.OK);
                                                response9.Content = new StringContent(cm.StatusTime(false, _output.ErrorDetails.ToString()), Encoding.UTF8, "application/json");

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
                                    //}
                                    //else
                                    //{
                                    //    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                    //    response.Content = new StringContent(cm.StatusTime(false, "Oops! Atleast Transporter Id or Transporter GSTNo,Transporter Name Required and distance, Thats missing!!!!!!!!"), Encoding.UTF8, "application/json");

                                    //    return response;
                                    //}
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
                                response.Content = new StringContent(cm.StatusTime(false, "Oops! Key not generated!!!!!!!!"), Encoding.UTF8, "application/json");

                                return response;
                            }
                        }
                        else
                        {
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Oops!"+ output), Encoding.UTF8, "application/json");

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
                catch (Exception ex)
                {

                    
                    if (distance == 0)
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


        public static string EWayGeneratebyInvoicevalidation(DataTable dr3)
        {
            string output = string.Empty;
            if(string.IsNullOrEmpty(dr3.Rows[0]["TransId"].ToString()))
            {
                output = "Not a valid transporter";               
            }
            else if (string.IsNullOrEmpty(dr3.Rows[0]["TransName"].ToString()))
            {
                output = "Not a valid TransporterName";
            }
            else if (string.IsNullOrEmpty(dr3.Rows[0]["TransDocNo"].ToString()))
            {
                output = "Not a valid Transport document no.";
            }
            else if (string.IsNullOrEmpty(dr3.Rows[0]["TransDocDt"].ToString()))
            {
                output = "Not a valid Transport Date";
            }
            else if (string.IsNullOrEmpty(dr3.Rows[0]["VehNo"].ToString()))
            {
                output = "Not a valid Vehical No.";
            }
            else if (string.IsNullOrEmpty(dr3.Rows[0]["ExpShipAddr1"].ToString()))
            {
                output = "Not a valid shipping Address";
            }
            else if (string.IsNullOrEmpty(dr3.Rows[0]["ExpShipLoc"].ToString()))
            {
                output = "Not a valid shipping location";
            }

            else if (string.IsNullOrEmpty(dr3.Rows[0]["ExpShipLoc"].ToString()))
            {
                output = "Not a valid shipping pincode";
            }

            else if (string.IsNullOrEmpty(dr3.Rows[0]["ExpShipStcd"].ToString()))
            {
                output = "Not a valid shipping state code";
            }
            return output;
        }
    }
}