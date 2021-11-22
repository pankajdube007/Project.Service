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
    public class AddPodController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/addpod")]
        public HttpResponseMessage GetDetails(ListAddPod ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    List<AddPods> alldcr = new List<AddPods>();
                    List<AddPod> alldcr1 = new List<AddPod>();

                    string data1;

                    string val = g2.reterive_val("updatepod '" + ula.uniqueKey + "','" + ula.CIN + "','" + ula.podat + "','" + ula.ispodt + "','" + ula.lat + "','" + ula.lan + "','" + ula.address + "','" + ula.Ip + "'");

                    if (val == "0")
                    {
                        alldcr1.Add(new AddPod
                        {
                            isResult = "SomeThing Wrong",
                        });

                        g2.close_connection();
                        alldcr.Add(new AddPods
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
                        alldcr1.Add(new AddPod
                        {
                            isResult = "Party Not Matched",
                        });

                        g2.close_connection();
                        alldcr.Add(new AddPods
                        {
                            result = false,
                            message = "Party Not Matched",
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
                        alldcr1.Add(new AddPod
                        {
                            isResult= "Pod Date Updated",
                        });

                        g2.close_connection();
                        alldcr.Add(new AddPods
                        {
                            result = true,
                            message = "Pod Date Updated",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else if (val == "3")
                    {
                        alldcr1.Add(new AddPod
                        {
                            isResult = "Pod Date Not Updated",
                        });

                        g2.close_connection();
                        alldcr.Add(new AddPods
                        {
                            result = true,
                            message = "Pod Date Not Updated",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else if (val == "4")
                    {
                        alldcr1.Add(new AddPod
                        {
                            isResult = "Invalid Invoice Or Already  Updated",
                        });
                        g2.close_connection();
                        alldcr.Add(new AddPods
                        {
                            result = false,
                            message = "Invalid Invoice Or Already  Updated",
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