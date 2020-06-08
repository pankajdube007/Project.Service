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
    public class EwayBillGeneratePart2Controller : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/EwayBillGeneratePartB")]
        public HttpResponseMessage GetDetails(EwayBillGeneratePart2 ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            var key = string.Empty;
            if (ula.ewaybillno != "")
            {
                try
                {
                   EwayBillGeneratePart2body body = new EwayBillGeneratePart2body();
                    key = cm.GetEwayTokanno();

                    if (key != "")
                    {

                        body.EwbNo = ula.ewaybillno;
                        body.VehicleNo = ula.vehicleno;
                        body.FromPlace = ula.fromplace;
                        body.FromState = ula.fromstate;
                        body.ReasonCode = ula.resoncode;
                        body.ReasonRem = ula.reasonrem;
                        body.TransDocNo = ula.transdocno;
                        body.TransDocDate = ula.transdocdate;
                        body.TransMode = ula.transmode;

                        var dr5 = g2.return_dt("EwayBillCheck '" + ula.ewaybillno+"'");
                        if (dr5.Rows.Count > 0)
                        {

                            string requtid = string.Empty;
                            requtid = dr5.Rows[0]["locnm"].ToString().Substring(0, 3) + cm.GenerateRandomNo(10);

                            var baseurl = ConfigurationManager.AppSettings["Gold.Eway.API"] + "VEHEWB";
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
                                var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(body).TrimEnd(']').TrimStart('[');
                                streamWriter.Write(json);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }

                            System.Net.HttpWebResponse response1 = (System.Net.HttpWebResponse)request1.GetResponse();
                            if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                dynamic _output = JsonConvert.DeserializeObject((new StreamReader(response1.GetResponseStream())).ReadToEnd());
                                g2.close_connection();
                                if ( _output.success=true && _output.message=="E-Way Bill is updated successfully")
                                {
                                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                    response.Content = new StringContent(cm.StatusTime(true, "EwayBill updated sucessfully!!!"), Encoding.UTF8, "application/json");

                                    return response;
                                }
                               else
                                {

                                    HttpResponseMessage response9 = Request.CreateResponse(HttpStatusCode.OK);
                                    response9.Content = new StringContent(cm.StatusTime(false, _output.message.ToString()), Encoding.UTF8, "application/json");

                                    return response9;
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