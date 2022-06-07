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
    public class ApprovalWeeklyOffController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getApprovalWeeklyOffList")]
        public HttpResponseMessage GetDetails(ListofApprovalWeeklyOff ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.EmpID != 0)
            {
                try
                {
                    string data1;

                    List<GetApprovalWeeklyOffLists> alldcr = new List<GetApprovalWeeklyOffLists>();
                    List<GetApprovalWeeklyOffList> alldcr1 = new List<GetApprovalWeeklyOffList>();
                    var dr = g1.return_dr("dbo.GetApprovalWeeklyOff '" + ula.EmpID + "'");
                    //var dr = g1.return_dr("dbo.GetApprovalWeeklyOff");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetApprovalWeeklyOffList
                            {

                                EmpID = Convert.ToString(dr["EmpID"].ToString()),
                                WeeklyOffDay = Convert.ToString(dr["WeeklyOffDay"].ToString()),
                                ApprovalStatus = Convert.ToString(dr["ApprovalStatus"].ToString()),
                                salesexnm = Convert.ToString(dr["salesexnm"].ToString()),
                                EmployeeLastName = Convert.ToString(dr["EmployeeLastName"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetApprovalWeeklyOffLists
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
                        alldcr.Add(new GetApprovalWeeklyOffLists
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