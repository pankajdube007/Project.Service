using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;


namespace Project.Service.Controllers
{
    public class CheckFreepayFaildTransController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/CheckFreePayFailedTranstions")]
        public HttpResponseMessage GetDetails(CheckFreepayFaildTrans ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    var dr1 = g2.return_dt("getfreepayidbytransid '" + ula.transactionid + "'");

                    if (dr1.Rows[0][0] != null || dr1.Rows[0][0].ToString() != "" || dr1.Rows[0][0].ToString() != "NULL")

                    {
                        Dictionary<string, string> header = new Dictionary<string, string>
                    {
                        { "x-cpg-code", ConfigurationManager.AppSettings["Gold.Freepay.Code"] },
                        { "x-dealer-code", ula.CIN.ToString()},
                        //{ "Accept", "application/json" },
                        //{ "Content-type", "application/json" }
                    };

                        var baseurl = ConfigurationManager.AppSettings["Gold.Freepay.API"] + "payment-status/" + dr1.Rows[0][0].ToString() + "?show-details=false";
                        RemoteStatus res;
                        using (var remoteClient = new RemoteClient())
                        {
                            res = remoteClient.GetAsync(url: baseurl,
                               customsHeader: header).Result;
                        }


                        dynamic _output = JsonConvert.DeserializeObject(res.Response);


                        if (res.StatusCode == 200)
                        {
                            if (_output.status.code == "200" && _output.data.paymentInfo.payexPayFlag.ToString() == "S401")
                            {
                                var dr2 = g2.return_dt("checkfailedtransaction '" + ula.transactionid + "','" + ula.CIN + "','Approved 1'");

                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(true, "Payment processed!!!!!!!"), Encoding.UTF8, "application/json");

                                return response;
                            }

                            else if (_output.status.code == "200" && _output.data.paymentInfo.payexPayFlag.ToString() == "S400")
                            {

                                var dr2 = g2.return_dt("checkfailedtransaction '" + ula.transactionid + "','" + ula.CIN + "','open'");
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(true, "Payment is open, pending for authorization!!!!!!!"), Encoding.UTF8, "application/json");

                                return response;
                            }
                            else
                            {
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "Payment not processed, please try again after some time!!!!!!!"), Encoding.UTF8, "application/json");

                                return response;
                            }

                        }
                        else
                        {

                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Payment not processed, please try again after some time!!!!!!!"), Encoding.UTF8, "application/json");

                            return response;

                        }
                    }
                    else
                    {

                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Payment not processed, please try again after some time!!!!!!!"), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}