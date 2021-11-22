using Newtonsoft.Json;
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
    public class EwayCancelAdaequareController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/CancelEWayBillAda")]
        public HttpResponseMessage GetDetails(ListofEWayBillCancelbyInvoice ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            var key = string.Empty;


            if (ula.userid != 0 && ula.slno != 0)
            {
                try
                {

                    key = cm.GetEInvoiceTokannoAdaequare();
                    var dr3 = g2.return_dt("EwayCancelDetails " + ula.slno + "," + ula.type);

                    if (key != "" && dr3.Rows.Count > 0)
                    {

                        var cancel = new EWayBillCancelbyInvoice
                        {
                            ewbNo = dr3.Rows[0]["EWayBillno"].ToString(),
                            //   ewbNo = "191260236288",
                            cancelRsnCode = 2,
                            cancelRmrk = "Wrong entry"
                        };


                        var dr5 = g2.return_dt("EwayBillGenerateHeader " + ula.slno + "," + ula.type);
                        if (dr5.Rows.Count > 0)
                        {

                            string requtid = string.Empty;
                            requtid = dr5.Rows[0]["locnm"].ToString().Substring(0, 3) + cm.GenerateRandomNo(10);

                            // Bellow API not working due to government portal
                            //  var baseurl = ConfigurationManager.AppSettings["Gold.Eway.EAPI"] + "ewayapi";


                            var baseurl = ConfigurationManager.AppSettings["Gold.Eway.API"] + "CANEWB";
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
                                var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cancel).TrimEnd(']').TrimStart('[');
                                streamWriter.Write(json);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }


                            System.Net.HttpWebResponse response1 = (System.Net.HttpWebResponse)request1.GetResponse();





                            if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                dynamic _output = JsonConvert.DeserializeObject((new StreamReader(response1.GetResponseStream())).ReadToEnd());



                                if (_output.success = true && _output.message == "E-Way Bill is cancelled successfully")
                                {
                                    var dr1 = g2.return_dr("Ewaybillcancel " + ula.slno + ",'" + dr3.Rows[0]["EWayBillno"].ToString() + "','" + ula.userid + "'");
                                    if (dr1.HasRows)
                                    {
                                        g2.close_connection();
                                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                        response.Content = new StringContent(cm.StatusTime(true, "EwayBill Cancelled!!!"), Encoding.UTF8, "application/json");

                                        return response;
                                    }
                                    else
                                    {
                                        g2.close_connection();
                                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                        response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,EwayBill Cancelled,Transtion not updated in our system!!!"), Encoding.UTF8, "application/json");

                                        return response;
                                    }
                                }
                                else
                                {
                                    g2.close_connection();
                                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                    response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,Transtion not completed!!!" + _output.message), Encoding.UTF8, "application/json");

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

                ///   for Envoice ////


                //try
                //{

                //    key = cm.GetEInvoiceTokannoAdaequare();
                //    var dr3 = g2.return_dt("EwayCancelDetails " + ula.slno + "," + ula.type);

                //    if (key != "" && dr3.Rows.Count > 0)
                //    {

                //        var cancel = new EWayBillCancelbyInvoice
                //        {
                //            ewbNo = dr3.Rows[0]["EWayBillno"].ToString(),
                //         //   ewbNo = "191260236288",
                //            cancelRsnCode = 2,
                //            cancelRmrk = "Wrong entry"
                //        };


                //        var dr5 = g2.return_dt("EwayBillGenerateHeader " + ula.slno);
                //        if (dr5.Rows.Count > 0)
                //        {

                //            string requtid = string.Empty;
                //            requtid = dr5.Rows[0]["locnm"].ToString().Substring(0, 3) + cm.GenerateRandomNo(10);

                //            var baseurl = ConfigurationManager.AppSettings["Gold.Eway.EAPI"] + "ewayapi";

                //            System.Net.HttpWebRequest request1 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(baseurl);

                //            // for production //
                //            request1.Method = "POST";
                //            request1.ContentType = "application/json";
                //            request1.Headers.Add("user_name", dr5.Rows[0]["userid"].ToString());
                //            request1.Headers.Add("password", dr5.Rows[0]["password"].ToString());
                //            request1.Headers.Add("gstin", dr5.Rows[0]["GSTNo"].ToString());
                //            request1.Headers.Add("requestid", requtid);
                //            request1.Headers.Add("Authorization", "Bearer " + key);

                //            // for testing //
                //            //request1.Method = "POST";
                //            //request1.ContentType = "application/json";
                //            //request1.Headers.Add("Username", "05AAACG2115R1ZN");
                //            //request1.Headers.Add("Password", "abc123@@");
                //            //request1.Headers.Add("gstin", "05AAACG2115R1ZN");
                //            //request1.Headers.Add("requestid", requtid);
                //            //request1.Headers.Add("Authorization", "Bearer " + key);

                //            //Place the serialized content of the object to be posted into the request stream
                //            using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                //            {
                //                var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cancel).TrimEnd(']').TrimStart('[');
                //                streamWriter.Write(json);
                //                streamWriter.Flush();
                //                streamWriter.Close();
                //            }


                //            System.Net.HttpWebResponse response1 = (System.Net.HttpWebResponse)request1.GetResponse();





                //            if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                //            {
                //                dynamic _output = JsonConvert.DeserializeObject((new StreamReader(response1.GetResponseStream())).ReadToEnd());



                //                if (_output.success = true && _output.message == "E-Way Bill is cancelled successfully")
                //                {
                //                    var dr1 = g2.return_dr("EwayInvoicecancelbyInvoice " + ula.slno + "," + ula.userid + "," + ula.type);
                //                    if (dr1.HasRows)
                //                    {
                //                        g2.close_connection();
                //                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                //                        response.Content = new StringContent(cm.StatusTime(true, "EwayBill Cancelled!!!"), Encoding.UTF8, "application/json");

                //                        return response;
                //                    }
                //                    else
                //                    {
                //                        g2.close_connection();
                //                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                //                        response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,EwayBill Cancelled,Transtion not updated in our system!!!"), Encoding.UTF8, "application/json");

                //                        return response;
                //                    }
                //                }
                //                else
                //                {
                //                    g2.close_connection();
                //                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                //                    response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,Transtion not completed!!!" + _output.message), Encoding.UTF8, "application/json");

                //                    return response;
                //                }

                //            }
                //            else
                //            {
                //                g2.close_connection();
                //                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                //                response.Content = new StringContent(cm.StatusTime(false, "Response not 200!!!"), Encoding.UTF8, "application/json");

                //                return response;
                //            }

                //        }
                //        else
                //        {
                //            HttpResponseMessage response9 = Request.CreateResponse(HttpStatusCode.OK);
                //            response9.Content = new StringContent(cm.StatusTime(false, "Oops! No Proper Header or Incorrect Userid and Password!!!!!!!!"), Encoding.UTF8, "application/json");

                //            return response9;

                //        }
                //    }

                //    else
                //    {
                //        g2.close_connection();
                //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                //        response.Content = new StringContent(cm.StatusTime(false, "Key not Available!!!"), Encoding.UTF8, "application/json");

                //        return response;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    if (ex.Message == "The remote server returned an error: (401) Unauthorized.")
                //    {
                //        var dr5 = g2.return_dt("EwayBillKeyInactive");
                //        //if(dr5.Rows.Count>0)
                //        //{

                //        //        GetDetails(ula);

                //        //}
                //    }
                //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                //    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                //    return response;
                //}
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Not a valid invoice or user"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}