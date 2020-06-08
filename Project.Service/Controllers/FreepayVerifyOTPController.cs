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
    public class FreepayVerifyOTPController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/VerifyFreePayOTP")]
        public HttpResponseMessage GetDetails(ListsofFreepayOTP ula)
        {
            Common cm = new Common();
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;
            if (ula.CIN!="")
            {
                try
                {
                   
                    Dictionary<string, string> header = new Dictionary<string, string>
                    {
                        { "x-cpg-code", ConfigurationManager.AppSettings["Gold.Freepay.Code"] },
                        { "x-dealer-code", ula.CIN.ToString()},
                        //{ "Accept", "application/json" },
                        //{ "Content-type", "application/json" }
                    };

                    var baseurl = ConfigurationManager.AppSettings["Gold.Freepay.API"] + "auth-otp";
                    RemoteStatus res;
                    using (var remoteClient = new RemoteClient())
                    {
                        res = remoteClient.PostAsync(url: baseurl,
                           requestParam: ula,
                           customsHeader: header).Result;
                    }


                      dynamic _output=JsonConvert.DeserializeObject(res.Response);
                    //dynamic _output = JObject.Parse(res);
                    //var _outputstatus = _output.status.code.ToString();

                    //    output.status.code

                    if (res.StatusCode == 200)
                    {
                        if(_output.status.code=="200")
                        {
                            var dr = g2.return_dr("spAddFreePayRegister '" + ula.CIN.ToString() + "'");

                            if (dr.HasRows)
                            {
                                g2.close_connection();
                                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(true, "Registration done successfully!!!!!!!!"), Encoding.UTF8, "application/json");

                                return response;
                            }
                            else
                            {
                                g2.close_connection();
                                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "CIN is incorrect,Registration Unsuccessfull!!!!!!!!"), Encoding.UTF8, "application/json");

                                return response;
                            }
                        }
                       

                        else
                        {
                            g2.close_connection();
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "OTP or CIN is Incorrect!!!!!!!!"), Encoding.UTF8, "application/json");

                            return response;
                        }

                    }
                    else
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "OTP or CIN is Incorrect!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }

                    
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" ), Encoding.UTF8, "application/json");

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