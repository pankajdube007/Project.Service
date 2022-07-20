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
    public class DashboardListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDashboardList")]
        public HttpResponseMessage GetDetails(ListofDashboard ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.EmpID != 0)
            {
                try
                {
                    string data1;

                    List<GetDashboardLists> alldcr = new List<GetDashboardLists>();
                    List<GetDashboardList> alldcr1 = new List<GetDashboardList>();
                    var dr = g1.return_dr("GetDashboardList'" + ula.EmpID + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetDashboardList
                            {

                                SlNo = Convert.ToString(dr["SlNo"].ToString()),
                                contactno = Convert.ToString(dr["contactno"].ToString()),
                                salesexnm = Convert.ToString(dr["salesexnm"].ToString()),
                                EmployeeLastName = Convert.ToString(dr["EmployeeLastName"].ToString()),
                                WeeklyDayOff = Convert.ToString(dr["WeeklyDayOff"].ToString()),
                                Status = Convert.ToString(dr["Status"].ToString()),
                                IsApproval = Convert.ToString(dr["IsApproval"].ToString()),
                                IsWeeklyoffset = Convert.ToString(dr["IsWeeklyoffset"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetDashboardLists
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
                        alldcr.Add(new GetDashboardLists
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