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
    public class BranchWiseAttCountController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getbranchwiseattcount")]
        public HttpResponseMessage GetDetails(ListBranchwiseAttCount ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BranchwiseAttCountLists> alldcr = new List<BranchwiseAttCountLists>();
                    List<BranchwiseAttCountList> alldcr1 = new List<BranchwiseAttCountList>();

                    var dr = g1.return_dr("[hrm].[branchwiseattendancelist]'" + ula.CIN + "','" + ula.Cat + "','" + ula.Date + "'");
                    //var dr = g1.return_dr("[branchwiseattendancelist]'" + ula.CIN + "','" + ula.Cat + "','" + ula.Date + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BranchwiseAttCountList
                            {
                                BranchId = Convert.ToInt32(dr["SlNo"].ToString()),
                                BranchName = Convert.ToString(dr["locnm"].ToString()),
                                SalesexecAbsent = Convert.ToString(dr["salesab"].ToString()),
                                SalesexecPresent = Convert.ToString(dr["salespre"].ToString()),
                                EmployeeAbsent = Convert.ToString(dr["internalab"].ToString()),
                                EmployeePresent = Convert.ToString(dr["internalpre"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BranchwiseAttCountLists
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