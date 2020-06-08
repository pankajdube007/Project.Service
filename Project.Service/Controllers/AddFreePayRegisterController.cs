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
    public class AddFreePayRegisterController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/appfreepayregister")]
        public HttpResponseMessage GetDetails(AddFreePayRegister ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<FreePayRegisters> alldcr = new List<FreePayRegisters>();
                    List<FreePayRegister> alldcr1 = new List<FreePayRegister>();

                    string val = g2.reterive_val(string.Format("exec spAddFreePayRegister'" + ula.CIN + "'"));

                    if (!string.IsNullOrEmpty(val))
                    {
                        alldcr1.Add(new FreePayRegister
                        {
                            output = val
                        });

                        g2.close_connection();
                        alldcr.Add(new FreePayRegisters
                        {
                            result = true,
                            message = val,
                            servertime = DateTime.Now.ToString(),
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
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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