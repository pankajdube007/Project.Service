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
using Newtonsoft.Json.Serialization;

namespace Project.Service.Controllers
{
    public class FreepaySendOTPController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetFreePayOTP")]
        public HttpResponseMessage GetDetails(ListofFreepaySendOTP ula)
        {

            Common cm = new Common();
            string data;
            var request = Request;
            DataConnectionTrans g2 = new DataConnectionTrans();
            if (ula.CIN != "")
            {
                try
                {
                    RemoteClient rc = new RemoteClient();
                    List<FreepaySendOTPs> alldcr = new List<FreepaySendOTPs>();
                    List<FreepaySendOTP> alldcr1 = new List<FreepaySendOTP>();
                    Dictionary<string, string> header = new Dictionary<string, string>();
                    header.Add("x-cpg-code", ConfigurationManager.AppSettings["Gold.Freepay.Code"].ToString());
                    header.Add("x-dealer-code", ula.CIN.ToString());
                    //header.Add("Accept", "application/json");
                    //header.Add("Content-type", "application/json");


                    var baseurl = ConfigurationManager.AppSettings["Gold.Freepay.API"].ToString() + "auth-otp";


                    RemoteStatus res;
                    using (var remoteClient = new RemoteClient())
                    {
                        res = remoteClient.GetAsync(url: baseurl,
                           customsHeader: header).Result;
                    }

                    dynamic _output = JsonConvert.DeserializeObject(res.Response);
                    //var msg = _output.status.code;

                    if (res.StatusCode == 200)
                    {
                        var dr2 = g2.return_dt("sppartycontactdetail '" + ula.CIN + "'");


                        if (dr2.Rows.Count > 0)
                        {
                            alldcr1.Add(new FreepaySendOTP
                            {
                                isRegistered = false,
                                mobile = dr2.Rows[0]["mobile"].ToString(),
                                email = dr2.Rows[0]["email"].ToString(),

                            });

                        }



                        alldcr.Add(new FreepaySendOTPs
                        {
                            result = true,
                            message = "OTP Send to Registered mobile no.",
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else if (res.StatusCode == 400 && _output.status.errorCode.ToString()== "F011")
                    {
                        string val = g2.reterive_val(string.Format("exec spAddFreePayRegister'" + ula.CIN + "'"));

                        alldcr1.Add(new FreepaySendOTP { isRegistered = true,mobile="",email="" }
                                               );


                        alldcr.Add(new FreepaySendOTPs
                        {
                            result = true,
                            message = "Already Registered",
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        alldcr1.Add(new FreepaySendOTP { isRegistered = false ,mobile="",email=""}
                                               );


                        alldcr.Add(new FreepaySendOTPs
                        {
                            result = false,
                            message = res.StatusCode.ToString()+"-"+res.Response.ToString(),
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data, Encoding.UTF8, "application/json");

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