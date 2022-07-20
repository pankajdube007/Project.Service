using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.IT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.IT
{
    public class CurrentWeeklyTeamController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getCurrentWeeklyTeamDetails")]
        public HttpResponseMessage GetDetails(ListofCurrentWeeklyTeamDetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.DeviceId != "")
             {
                try
                {
                    string data1;

                    List<GetCurrentWeeklyTeamDetailsLists> alldcr = new List<GetCurrentWeeklyTeamDetailsLists>();
                    List<GetCurrentWeeklyTeamDetailsList> alldcr1 = new List<GetCurrentWeeklyTeamDetailsList>();
                    var dr = g1.return_dr("GetCurrentWeeklyTeamDetails");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetCurrentWeeklyTeamDetailsList
                            {
                                EmpID = Convert.ToString(dr["EmpID"].ToString()),
                                WeeklyOffDay = Convert.ToString(dr["WeeklyOffDay"].ToString()),
                                MonDate = Convert.ToString(dr["MonDate"].ToString()),
                                ApprovalStatus = Convert.ToString(dr["ApprovalStatus"].ToString()),
                                FirstName = Convert.ToString(dr["FirstName"].ToString()),
                                EmployeeLastName = Convert.ToString(dr["EmployeeLastName"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetCurrentWeeklyTeamDetailsLists
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
                        alldcr.Add(new GetCurrentWeeklyTeamDetailsLists
                        {
                            result = true,
                            message = "No Data available",
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");


                        return response;
                    }
                    //else
                    //{
                    //    g1.close_connection();
                    //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    //    response.Content = new StringContent(cm.StatusTime(true, "No Data available"), Encoding.UTF8, "application/json");

                    //    return response;
                    //}
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