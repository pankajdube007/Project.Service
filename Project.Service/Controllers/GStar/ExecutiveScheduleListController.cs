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
    public class ExecutiveScheduleListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetExecutiveSchduleList")]
        public HttpResponseMessage GetDetails(ListsofExecutiveScheduleList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecutiveScheduleLists> alldcr = new List<ExecutiveScheduleLists>();
                    List<ExecutiveScheduleList> alldcr1 = new List<ExecutiveScheduleList>();
                    var dr = g1.return_dr("ExecutiveScheduleLists  " + ula.ExId + "," + ula.OrgId + "," + ula.OrgCat + ",'"+ ula.EmpType + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecutiveScheduleList
                            {
                                slno = Convert.ToInt32(dr["slno"]),
                                date = Convert.ToString(dr["date1"]),
                                time = Convert.ToString(dr["dcrtime"]),
                                contactmodeid = Convert.ToString(dr["modeid"]),
                                contactmodename = Convert.ToString(dr["contactmodename"]),
                                partycatid = Convert.ToString(dr["productcategoryid"]),
                                partycatname = Convert.ToString(dr["partycatnm"]),
                                organizationid = Convert.ToString(dr["organizationid"]),
                                organizationname = Convert.ToString(dr["compname"]),
                                productcatid = Convert.ToString(dr["productcategoryid"]),
                                productcatname = Convert.ToString(dr["productnm"]).TrimStart(','),
                                addressid = Convert.ToString(dr["addressid"]),
                                addressname = Convert.ToString(dr["address"]),
                                contactpersonid = Convert.ToString(dr["cpid"]),
                                contactperson = Convert.ToString(dr["cp"]).TrimStart(','),
                                purposeid = Convert.ToString(dr["purposeid"]),
                                purposename = Convert.ToString(dr["purpose"]).TrimStart(','),
                                priority = Convert.ToString(dr["dcrpriority"]),
                                remark = Convert.ToString(dr["remark"]),
                                reason = Convert.ToString(dr["reason"]),
                                status = Convert.ToString(dr["stat"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecutiveScheduleLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data available"), Encoding.UTF8, "application/json");

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