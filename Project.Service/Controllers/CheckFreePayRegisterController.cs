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
    public class CheckFreePayRegisterController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/CheckFreePayRegister")]
        public HttpResponseMessage GetDetails(ListCheckFreePayRegister ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<CheckFreePayRegisters> alldcr = new List<CheckFreePayRegisters>();
                    List<CheckFreePayRegister> alldcr1 = new List<CheckFreePayRegister>();

                    var dr = g1.return_dr("spCheckFreePayRegister " + ula.CIN + "");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new CheckFreePayRegister
                            {
                                status = Convert.ToBoolean(dr["IsActive"]),
                                desc = Convert.ToString(dr["errormsg"]),
                                duesequence = Convert.ToBoolean(dr["duesquence"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CheckFreePayRegisters
                        {
                            result = true,
                            message = string.Empty,
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
                       

                        alldcr1.Add(new CheckFreePayRegister
                        {
                            status = false,
                            desc = "",
                            duesequence = false,
                        });
                        g1.close_connection();

                        alldcr.Add(new CheckFreePayRegisters
                        {
                            result = false,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

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