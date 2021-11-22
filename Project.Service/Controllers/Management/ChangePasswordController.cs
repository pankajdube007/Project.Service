using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
namespace Project.Service.Controllers
{
    public class ChangePasswordController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/chagnepassword")]
        public HttpResponseMessage GetDetails(ListChangePassword ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    List<ChangePasswords> alldcr = new List<ChangePasswords>();
                    List<ChangePassword> alldcr1 = new List<ChangePassword>();

                    string data1;

                    string val = g2.reterive_val("passchangemanagement '" + ula.Category + "','" + ula.CIN + "','" + ula.OldPassword + "','" + ula.NewPassword + "'");

                    if (val == "0")
                    {
                        alldcr1.Add(new ChangePassword
                        {
                            isResult = "SomeThing Wrong",
                        });

                        g2.close_connection();
                        alldcr.Add(new ChangePasswords
                        {
                            result = false,
                            message = "SomeThing Wrong",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;

                    }
                    else if (val == "1")
                    {
                        alldcr1.Add(new ChangePassword
                        {
                            isResult = "Old Password  Not Matched",
                        });

                        g2.close_connection();
                        alldcr.Add(new ChangePasswords
                        {
                            result = false,
                            message = "Old  Password  Not Matched",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;


                    }
                    else if (val == "2")
                    {
                        alldcr1.Add(new ChangePassword
                        {
                            isResult = "Password Changed",
                        });

                        g2.close_connection();
                        alldcr.Add(new ChangePasswords
                        {
                            result = true,
                            message = "Password Changed",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;


                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Invalid mpin."), Encoding.UTF8, "application/json");
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