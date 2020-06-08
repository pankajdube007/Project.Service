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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;

namespace Project.Service.Controllers
{
    public class PartyDeactivateController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/PartyDeactive")]
        public HttpResponseMessage GetAllUserdetails(PartyStatusInput ula)
        {
            //  DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;
                List<PartyStatuss> alldcr = new List<PartyStatuss>();
                List<PartyStatus> alldcr1 = new List<PartyStatus>();
                //  List<Cloud> alldcr2 = new List<Cloud>();
                List<PartyStop> stop = new List<PartyStop>();




                string baseurl = ConfigurationManager.AppSettings["Gold.Party.url"].ToString() + "halt";

                //For Party 1
                var client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers.Add("API-Key", ConfigurationManager.AppSettings["Gold.Party.Key1"].ToString());
                var response1 = client.UploadString(baseurl, "SUBID=" + ConfigurationManager.AppSettings["Gold.Party.id1"].ToString());
                dynamic _output = JsonConvert.DeserializeObject(response1);


                //For Party 2
                var client2 = new WebClient();
                client2.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client2.Headers.Add("API-Key", ConfigurationManager.AppSettings["Gold.Party.Key2"].ToString());
                var response2 = client2.UploadString(baseurl, "SUBID=" + ConfigurationManager.AppSettings["Gold.Party.id2"].ToString());
                dynamic _output2 = JsonConvert.DeserializeObject(response2);



                if (_output== null && _output2 == null)
                {

                    alldcr1.Add(new PartyStatus
                    {
                        status = "deactive",
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
                    //  g1.close_connection();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong or Party deactivated already, try again later!!!!!!!!"+ex.Message), Encoding.UTF8, "application/json");

                return response;
            }

        }
    }
}