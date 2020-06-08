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
namespace Project.Service.Controllers
{
    public class PaymentRequestResendOTPController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetResendPaymentRequestOTP")]
        public HttpResponseMessage GetDetails(PaymentRequestResendOTP ula)
        {

            Common cm = new Common();
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;
            if (ula.CIN != "")
            {
                try
                {
                    RemoteClient rc = new RemoteClient();
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("x-cpg-code", ConfigurationManager.AppSettings["Gold.Freepay.Code"].ToString());
                    header.Add("x-dealer-code", ula.CIN.ToString());
                    //header.Add("Accept", "application/json");
                    //header.Add("Content-type", "application/json");
                    var freepaytransid = g2.reterive_val("FreepayidbyTransid " + ula.transactionid);

                    if (freepaytransid != "")
                    {
                        var baseurl = ConfigurationManager.AppSettings["Gold.Freepay.API"].ToString() + "resend-txn-otp/" + freepaytransid;
                        RemoteStatus res;
                        using (var remoteClient = new RemoteClient())
                        {
                            res = remoteClient.GetAsync(url: baseurl,
                               customsHeader: header).Result;
                        }

                        //dynamic _output = JsonConvert.DeserializeObject(res.Response);
                        //var msg = _output.status.code;

                        if (res.StatusCode == 200)
                        {
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(true, "OTP Send successfully on Registered mobile.!!!!!!"), Encoding.UTF8, "application/json");

                            return response;
                        }
                        else
                        {
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "OTP Not Send, Please try again after some time!!!!!!"), Encoding.UTF8, "application/json");

                            return response;
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "OTP Not Send, Not a valid transaction id!!!!!!"), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}