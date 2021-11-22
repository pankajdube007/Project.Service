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
    public class JointDateWiseEmployeeDataController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getjoindatewiseempdata")]
        public HttpResponseMessage GetDetails(ListJointDateWiseEmployeeData ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<JointDateWiseEmployeeDataLists> alldcr = new List<JointDateWiseEmployeeDataLists>();
                    List<JointDateWiseEmployeeDataList> alldcr1 = new List<JointDateWiseEmployeeDataList>();

                    var dr = g1.return_dr("[hrm].[JointDateWiseEmployeeData] '" + ula.CIN + "','" + ula.Cat + "','" + ula.fromdate + "','" + ula.todate + "','" + ula.type + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new JointDateWiseEmployeeDataList
                            {
                                EmployeeName = Convert.ToString(dr["EmployeeName"].ToString()),
                                EmployeeCode = Convert.ToString(dr["EmployeeCode"].ToString()),
                                MobileNumber = Convert.ToString(dr["MobileNumber"].ToString()),
                                DepartmentName = Convert.ToString(dr["DepartmentName"].ToString()),
                                DesignationName = Convert.ToString(dr["DesignationName"].ToString()),
                                Branchname = Convert.ToString(dr["Branchname"].ToString()),
                                Location = Convert.ToString(dr["Location"].ToString()),
                                SubLocation = Convert.ToString(dr["SubLocation"].ToString()),
                                WorkYear = Convert.ToString(dr["wrkyear"].ToString()),
                                slno = Convert.ToInt32(dr["slno"].ToString()),
                                JoinDate = Convert.ToString(dr["joindate"].ToString()),
                                Leavedate = Convert.ToString(dr["lvdt"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new JointDateWiseEmployeeDataLists
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