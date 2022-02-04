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
using System.Web.Script.Serialization;

namespace Project.Service.Controllers
{
    public class EInvoiceCancelController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/CancelEInvoiceBill")]
        public HttpResponseMessage GetDetails(ListofEInvoiceCancel ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            var key = new EinvoiceKey();
            if (ula.userid != 0 && ula.slno != 0)
            {
                try     
                {
               
                   var dr3 = g2.return_dt("EwayCancelDetails " + ula.slno +","+ ula.type);
                    if (dr3.Rows.Count>0)
                    {

                        var cancel =new EInvoiceCancel
                        {
                            irn = dr3.Rows[0]["Irn"].ToString(),
                            cnlrsn = "1",
                            cnlrem = "Wrong entry"
                        };


                             // key = cm.GetEInvoiceTokanno(dr3.Rows[0]["GSTUser"].ToString(), dr3.Rows[0]["GSTPassword"].ToString(), dr3.Rows[0]["ClientID"].ToString(), dr3.Rows[0]["ClientSecret"].ToString());
                            key = cm.GetEInvoiceTokanno("goldluein", "111@Abc@", "AACCG09TXPMK3F7", "Ppd4J8XLl2zFeTshMBa1");

                         

                            var baseurl = ConfigurationManager.AppSettings["Gold.Invoice.API"] + "eicore/v1.03/Invoice/Cancel";
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



                        var json1 = new JavaScriptSerializer().Serialize(cancel);

                        var datatocall = cm.EncryptBySymmetricKey(json1, key.Sek);


                        RootInvoice root = new RootInvoice
                        {
                            Data = datatocall
                        };


                        //Place the serialized content of the object to be posted into the request stream
                        using (var streamWriter = new StreamWriter(request1.GetRequestStream()))
                            {
                                var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(root).TrimEnd(']').TrimStart('[');
                                streamWriter.Write(json);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }


                            System.Net.HttpWebResponse response1 = (System.Net.HttpWebResponse)request1.GetResponse();





                            if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                dynamic _output = JsonConvert.DeserializeObject((new StreamReader(response1.GetResponseStream())).ReadToEnd());


                                
                                if (Convert.ToInt32(_output.Status) == 1)
                                {
                                var dr1 = g2.return_dr("EwayInvoicecancel " + ula.slno + "," + ula.userid +","+ula.type);
                                if (dr1.HasRows)
                                    {
                                        g2.close_connection();
                                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                        response.Content = new StringContent(cm.StatusTime(true, "EInvoice Cancelled!!!"), Encoding.UTF8, "application/json");

                                        return response;
                                    }
                                    else
                                    {
                                        g2.close_connection();
                                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                        response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,Transtion not cancelled!!!"), Encoding.UTF8, "application/json");

                                        return response;
                                    }
                                }
                                else
                                {
                                var dr9 = g2.return_dt("EInvoiceinvalidtokan " + "goldluein");
                                // var dr9 = g2.return_dt("EInvoiceinvalidtokan '" + dr3.Rows[0]["GSTUser"].ToString() + "'");
                                //  g2.close_connection();
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                    response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,Transtion not cancelled!!!"+ _output.ErrorDetails.ToString()), Encoding.UTF8, "application/json");

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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Not a Active Invoice!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                   
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