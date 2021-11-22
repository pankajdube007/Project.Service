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
    public class EmployeeListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getEmployeeList")]
        public HttpResponseMessage GetDetails(ListEmployeeList ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<EmployeeLists> alldcr = new List<EmployeeLists>();
                    List<EmployeeList> alldcr1 = new List<EmployeeList>();

                    var dr = g1.return_dr("[hrm].[EmployeeList] '" + ula.CIN + "','" + ula.Cat + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new EmployeeList
                            {
                                EmployeeCount = Convert.ToInt32(dr["EmployeeCount"].ToString()),
                                InternalEmpCount = Convert.ToInt32(dr["intercnt"].ToString()),
                                ExecCount = Convert.ToInt32(dr["execcnt"].ToString()),
                                BranchName = Convert.ToString(dr["BranchName"].ToString()),
                                BranchId= Convert.ToInt32(dr["BrachId"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new EmployeeLists
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