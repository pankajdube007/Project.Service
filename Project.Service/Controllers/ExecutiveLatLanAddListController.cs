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
    public class ExecutiveLatLanAddListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ListAddressLatLonExeAdd")]
        public HttpResponseMessage GetDetails(Executivelatlanadd ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecutivelatlanaddLists> alldcr = new List<ExecutivelatlanaddLists>();
                    List<ExecutivelatlanaddList> alldcr1 = new List<ExecutivelatlanaddList>();

                    var dr = g1.return_dr("Listexeclatlanhomeoffice " + ula.ExId + "");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecutivelatlanaddList
                            {
                                officelat = Convert.ToString(dr["officelat"].ToString()),
                                officelan = Convert.ToString(dr["officelan"].ToString()),
                                officeadd = Convert.ToString(dr["officeadd"].ToString()),
                                homelat = Convert.ToString(dr["homelat"].ToString()),
                                homeadd = Convert.ToString(dr["homeadd"].ToString()),
                                homelan = Convert.ToString(dr["homelan"].ToString()),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecutivelatlanaddLists
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
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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