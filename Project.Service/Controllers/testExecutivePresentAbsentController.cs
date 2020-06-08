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
    public class testExecutivePresentAbsentController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GettestExecutivePresentAbsentList")]
        public HttpResponseMessage GetDetails(ExecutivePresentAbsent ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<testExecutivePresentAbsentLists> alldcr = new List<testExecutivePresentAbsentLists>();
                    List<testExecutivePresentAbsentList> alldcr1 = new List<testExecutivePresentAbsentList>();

                    var dr = g1.return_dr("ExecutivePresentAbsenttest " + ula.ExId + "");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new testExecutivePresentAbsentList
                            {
                                Attendance = Convert.ToString(dr["Attendance"].ToString()),
                                AttendanceStatus = Convert.ToString(dr["AttendanceStatus"].ToString()),
                                datetime = Convert.ToString(dr["dttime"].ToString()),
                                lat = Convert.ToString(dr["CheckinLat"].ToString()),
                                Long = Convert.ToString(dr["CheckinLong"].ToString()),
                                Address = Convert.ToString(dr["address"].ToString()),
                                islock = Convert.ToInt32(dr["islock"].ToString()),

                                orgname = Convert.ToString(dr["orgnm"].ToString()),
                                orgid = Convert.ToString(dr["orgid"].ToString()),
                                catid = Convert.ToString(dr["orgcat"].ToString()),
                                catname = Convert.ToString(dr["catname"].ToString()),
                                checkinoutstatus = Convert.ToInt32(dr["checkinoutstatus"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new testExecutivePresentAbsentLists
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