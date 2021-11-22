using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Project.Service.Controllers
{
    public class ConfirmPaymentRequestController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ConfirmPaymentRequest")]
        public HttpResponseMessage GetDetails(ListofConfirmPaymentRequest ula)
        {
            Common cm = new Common();
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;
            if (ula.CIN != "")
            {
                try
                {
                    //  List<ConfirmPaymentRequest> alldcr = new List<ConfirmPaymentRequest>();
                    var freepaytransid = g2.reterive_val("FreepayidbyTransid " + ula.transactionid);
                    //  var freepaytransid = "GPX_448";

                    var alldcr = new ConfirmPaymentRequest
                    {
                        freepayTxnId = freepaytransid,
                        otp = ula.otp
                    };

                    if (freepaytransid != "")
                    {
                        Dictionary<string, string> header = new Dictionary<string, string>
                    {
                        { "x-cpg-code", ConfigurationManager.AppSettings["Gold.Freepay.Code"] },
                        { "x-dealer-code", ula.CIN.ToString() },
                        //{ "Accept", "application/json" },
                        //{ "Content-type", "application/json" }
                    };

                        var baseurl = ConfigurationManager.AppSettings["Gold.Freepay.API"] + "auth-payments";
                        RemoteStatus res;
                        using (var remoteClient = new RemoteClient())
                        {
                            res = remoteClient.PostAsync(url: baseurl,
                               requestParam: alldcr,
                               customsHeader: header).Result;
                        }


                        dynamic _output = JsonConvert.DeserializeObject(res.Response);
                        //dynamic _output = JObject.Parse(res);
                        //var _outputstatus = _output.status.code.ToString();

                        //    output.status.code

                        if (res.StatusCode == 200)
                        {
                            try
                            {
                                var dr = g2.return_dr("FreePayConfirmPaymentRequest '" + ula.transactionid + "','" + ula.CIN + "'");

                                if (dr.HasRows)
                                {
                                    g2.close_connection();
                                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                    response.Content = new StringContent(cm.StatusTime(true, "Payment Processed successfully!!!!!!!!"), Encoding.UTF8, "application/json");
                                    return response;
                                }
                                else
                                {
                                    g2.close_connection();
                                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");
                                    return response;
                                }
                            }
                            catch (Exception)
                            {


                                g2.close_connection();
                                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");
                                return response;

                            }
                            
                        }


                        else if (res.StatusCode == 400 && _output.status.errorCode.ToString() == "F113")
                        {
                            g2.close_connection();
                            HttpResponseMessage response1 = request.CreateResponse(HttpStatusCode.OK);
                            response1.Content = new StringContent(cm.StatusTime(false, "Oops! Transaction PIN is not valid.!!!!!!!!"), Encoding.UTF8, "application/json");

                            return response1;
                        }
                        else if (res.StatusCode == 400 && _output.status.errorCode.ToString() == "F115")
                        {
                            g2.close_connection();
                            HttpResponseMessage response1 = request.CreateResponse(HttpStatusCode.OK);
                            response1.Content = new StringContent(cm.StatusTime(false, "Oops! OTP expired while authorizing payments.!!!!!!!!"), Encoding.UTF8, "application/json");

                            return response1;
                        }

                        else if (res.StatusCode == 511)
                        {

                            g2.close_connection();
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, _output.message.ToString()), Encoding.UTF8, "application/json");
                            return response;
                        }

                        else
                        {
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Payment not done, please try again!!!!!!!!"), Encoding.UTF8, "application/json");
                            return response;
                        }
                    }

                    else
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Payment not done, Transactionid Invalid!!!!!!!!"), Encoding.UTF8, "application/json");
                        return response;
                    }




                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}