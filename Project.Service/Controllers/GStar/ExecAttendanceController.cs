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
    public class ExecAttendanceController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetExeAttList")]
        public HttpResponseMessage GetDetails(ListExecAttDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecAttDetailLists> alldcr = new List<ExecAttDetailLists>();
                    List<ExecAttDetailList> alldcr1 = new List<ExecAttDetailList>();

                    var dr = g1.return_dr("allexeattdetailsbyexecutive " + ula.ExId + ",'" + ula.date + "','"+ula.Type+"'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecAttDetailList
                            {
                                ExecutiveName = Convert.ToString(dr["salesexnm"].ToString()),
                                Attendance = Convert.ToString(dr["attt"].ToString()),
                                AttendanceTime = Convert.ToString(dr["atttime"].ToString()),
                                OutTime = Convert.ToString(dr["outime"].ToString()),
                                InLatitude = Convert.ToString(dr["chkinlat"].ToString()),
                                InLongitude = Convert.ToString(dr["chkinlng"].ToString()),
                                OutLatitude = Convert.ToString(dr["chkoutlat"].ToString()),
                                OutLongitude = Convert.ToString(dr["chkoutlng"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecAttDetailLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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