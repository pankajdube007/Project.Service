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
using System.Configuration;

namespace Project.Service.Controllers
{
    public class PartyStatusController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getPartyStatus")]
        public HttpResponseMessage GetAllUserdetails(PartyStatusInput ula)
        {
           
            Common cm = new Common();

            try
            {
                string data1;
                List<PartyStatuss> alldcr = new List<PartyStatuss>();
                List<PartyStatus> alldcr1 = new List<PartyStatus>();


                // For Party 1
                string baseurl = ConfigurationManager.AppSettings["Gold.Party.url"].ToString() + "list"; ;
                var client = new WebClient();
                client.Headers.Add("API-Key", ConfigurationManager.AppSettings["Gold.Party.Key1"].ToString());
                var response1 = client.DownloadString(baseurl).Replace(ConfigurationManager.AppSettings["Gold.Party.id1"], "header");
                dynamic _output = JsonConvert.DeserializeObject(response1);


                // For Party 2
                var client2 = new WebClient();
                client2.Headers.Add("API-Key", ConfigurationManager.AppSettings["Gold.Party.Key2"].ToString());
                var response2 = client2.DownloadString(baseurl).Replace(ConfigurationManager.AppSettings["Gold.Party.id2"], "header");
                dynamic _output2 = JsonConvert.DeserializeObject(response2);

                if (_output.ToString() != "" && _output2.ToString() != "")
                {
                    string output = string.Empty;
                

                    if (_output.header.power_status == "running" || _output2.header.power_status == "running")
                    {
                        output = "active";
                    }
                    else
                    {
                        output = "deactive";
                    }

                 

                    alldcr1.Add(new PartyStatus
                    {
                        status = output
                    });


                    alldcr.Add(new PartyStatuss
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

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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