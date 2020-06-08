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
    public class UpdateIncreaseLimitController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/UpdateIncreaseLimitParty")]
        public HttpResponseMessage GetDetails(UpdateIncreaseLimitAction ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.cin != string.Empty)
            {
                try
                {
                    string data;
                    List<UpdateIncreaseLimits> alldcr = new List<UpdateIncreaseLimits>();
                    List<IncreaseLimit> alldcr1 = new List<IncreaseLimit>();
                    var dr = g2.return_dr("App_UpdateLimitAdd " + ula.userid + ",'" + ula.cin + "'," + ula.limitamt);
                    if (dr.HasRows)
                    {
                        g2.close_connection();
                        alldcr.Add(new UpdateIncreaseLimits
                        {
                            result = "True",
                            message = "Updated Successfully",
                            servertime = DateTime.Now.ToString(),
                            data = string.Empty,
                        });
                        data = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g2.close_connection();
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