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
    public class JoinDateWiseEmpCountValuesController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getjoindatewiseempcount")]
        public HttpResponseMessage GetDetails(ListJointDateWiseEmployeeCount ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<JointDateWiseEmployeeCountLists> alldcr = new List<JointDateWiseEmployeeCountLists>();
                    List<JointDateWiseEmployeeCountList> alldcr1 = new List<JointDateWiseEmployeeCountList>();

                    var dr = g1.return_dr("[hrm].[JointDateWiseEmployeeCount] '" + ula.CIN + "','" + ula.Cat + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new JointDateWiseEmployeeCountList
                            {
                                YearCount = Convert.ToInt32(dr["YearCount"].ToString()),
                                MonthCount = Convert.ToInt32(dr["MonthCount"].ToString()),
                                MonthCountLeaving = Convert.ToInt32(dr["MonthCountleaving"].ToString()),
                                YearCountLeaving = Convert.ToInt32(dr["YearCountleaving"].ToString()),
                                TotalCount = Convert.ToInt32(dr["totalcount"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new JointDateWiseEmployeeCountLists
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