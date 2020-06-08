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
    public class IncreaseLimitController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetIncreaseLimitParty")]
        public HttpResponseMessage GetDetails(IncreaseLimitAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.partyid != 0)
            {
                try
                {
                    string data;
                    List<IncreaseLimits> alldcr = new List<IncreaseLimits>();
                    List<IncreaseLimit> alldcr1 = new List<IncreaseLimit>();
                    var dr = g1.return_dr("App_IncreaseLimitParty " + ula.partyid + ",'" + ula.searchtxt + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new IncreaseLimit
                            {
                                cin = Convert.ToString(dr["cin"].ToString()),
                                displaynm = Convert.ToString(dr["displaynm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new IncreaseLimits
                        {
                            result = "True",
                            message = string.Empty,
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
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No  Data available"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}