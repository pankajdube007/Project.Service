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

namespace Project.Service.Controllers.Management
{
    public class GetGroupwisePassengerCountController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getGroupwisePassengerCount")]
        public HttpResponseMessage GetDetails(ListofGroupwisePassengerCount ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;


                    List<GroupwisePassengerCounts> alldcr = new List<GroupwisePassengerCounts>();
                    List<GroupwisePassengerCount> alldcr1 = new List<GroupwisePassengerCount>();
                    var dr = g1.return_dr("GetGroupwisePassengerCount ");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GroupwisePassengerCount
                            {
                                GroupName = Convert.ToString(dr["GroupName"]),
                                UserType = Convert.ToString(dr["UserType"]),
                                PassangerCount = Convert.ToString(dr["PassangerCount"].ToString()),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GroupwisePassengerCounts
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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