using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class ServerTimeController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getServerTime")]
        public HttpResponseMessage GetDetails(ListsofServerTime input)
        {
     
            Common cm = new Common();

            try
            {
                string data1;
                List<ServerTime> alldcr = new List<ServerTime>();   
                
                if(input.ExId!=0)
                {

                    alldcr.Add(new ServerTime
                    {
                        result = true,
                        servertime = DateTime.Now.ToString(),
                        message = string.Empty,
                        data = null
                    });
                    data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;

                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Login First!!!!!!!!"), Encoding.UTF8, "application/json");

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
    }
}