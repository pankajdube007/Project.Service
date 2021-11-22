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

namespace Project.Service.Controllers
{
    public class EWaybyInvoiceAdaequareController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/EwayGeneratebyInvoiceAda")]
        public HttpResponseMessage GetDetails(ListsofEwayGeneratebyEInvoice ula)
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

                    key = cm.GetEInvoiceTokannoAdaequare();
                    var dr3 = g2.return_dt("EInvoiceSubDatabySlno " + ula.slno + "," + ula.type);

                    if (key != "" && dr3.Rows.Count > 0)
                    {
                        string output = EWayGeneratebyInvoicevalidation(dr3);

                        if (output == "")
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




                            var dr5 = g2.return_dt("EwayBillGenerateHeader " + ula.slno + "," + ula.type);
                            if (dr5.Rows.Count > 0)
                            {

                                string requtid = string.Empty;
                                requtid = dr5.Rows[0]["locnm"].ToString().Substring(0, 3) + cm.GenerateRandomNo(10);

                                var baseurl = ConfigurationManager.AppSettings["Gold.Eway.EAPI"] + "ewaybill";
                                System.Net.HttpWebRequest request1 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(baseurl);

                                // for production //
                                request1.Method = "POST";
                                request1.ContentType = "application/json";
                                request1.Headers.Add("user_name", dr5.Rows[0]["userid"].ToString());
                                request1.Headers.Add("password", dr5.Rows[0]["password"].ToString());
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
                                    var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data1).TrimEnd(']').TrimStart('[');
                                    streamWriter.Write(json);
                                    streamWriter.Flush();
                                    streamWriter.Close();
                                }


                                System.Net.HttpWebResponse response1 = (System.Net.HttpWebResponse)request1.GetResponse();





                                if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    dynamic _output = JsonConvert.DeserializeObject((new StreamReader(response1.GetResponseStream())).ReadToEnd());



                                    if (_output.success = true && _output.message == "E-Way Bill is generated successfully")
                                    {
                                        var sqlstring = ula.userid+","+ ula.slno + "," + ula.type + ",'"
                                      + _output.result.EwbNo.ToString() + "','"
                                      + _output.result.EwbDt.ToString() + "','"
                                      + _output.result.EwbValidTill.ToString() + "'";

                                        var dr1 = g2.return_dr("EwaybillInvoiceupdatebyAPI " + sqlstring);


                                        if (dr1.HasRows)
                                        {
                                            g2.close_connection();
                                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                            response.Content = new StringContent(cm.StatusTime(true, "E-Way Bill is generated successfully!!!"), Encoding.UTF8, "application/json");

                                            return response;
                                        }
                                        else
                                        {
                                            g2.close_connection();
                                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                            response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,Transtion not completed!!!"+ _output.message), Encoding.UTF8, "application/json");

                                            return response;
                                        }
                                    }
                                    else
                                    {
                                        g2.close_connection();
                                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                        response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,Transtion not completed!!!" +_output.message), Encoding.UTF8, "application/json");

                                        return response;
                                    }

                                }
                                else
                                {
                                    g2.close_connection();
                                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                    response.Content = new StringContent(cm.StatusTime(false, "Response not 200!!!"), Encoding.UTF8, "application/json");

                                    return response;
                                }

                            }
                            else
                            {
                                HttpResponseMessage response9 = Request.CreateResponse(HttpStatusCode.OK);
                                response9.Content = new StringContent(cm.StatusTime(false, "Oops! No Proper Header or Incorrect Userid and Password!!!!!!!!"), Encoding.UTF8, "application/json");

                                return response9;

                            }
                        }
                        else
                        {
                            HttpResponseMessage response9 = Request.CreateResponse(HttpStatusCode.OK);
                            response9.Content = new StringContent(cm.StatusTime(false, "Oops!"+output), Encoding.UTF8, "application/json");

                            return response9;
                        }

                    }

                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Key not Available!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "The remote server returned an error: (401) Unauthorized.")
                    {
                        var dr5 = g2.return_dt("EwayBillKeyInactive");
                        //if(dr5.Rows.Count>0)
                        //{

                        //        GetDetails(ula);

                        //}
                    }
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                    return response;
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
            if (string.IsNullOrEmpty(dr3.Rows[0]["TransId"].ToString()))
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