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
    public class DepartmentwiseempdetailController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getdepartmentwiseempdetail")]
        public HttpResponseMessage GetDetails(ListDepartmentwiseempdetail ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<DepartmentwiseempdetailLists> alldcr = new List<DepartmentwiseempdetailLists>();
                    List<DepartmentwiseempdetailList> alldcr1 = new List<DepartmentwiseempdetailList>();

                    var dr = g1.return_dr("[hrm].[DepartmentWiseEmployeeData]'" + ula.CIN + "','" + ula.Cat + "','"+ula.DeptId+"'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DepartmentwiseempdetailList
                            {
                                EmployeeName = Convert.ToString(dr["EmployeeName"].ToString()),
                                Departmentname = Convert.ToString(dr["Departmentname"].ToString()),
                                DesignationName = Convert.ToString(dr["DesignationName"].ToString()),
                                EmployeeCode = Convert.ToString(dr["EmployeeCode"].ToString()),
                                Branchname = Convert.ToString(dr["Branchname"].ToString()),
                                MobileNumber = Convert.ToString(dr["MobileNumber"].ToString()),
                                JoinDate = Convert.ToString(dr["joindate"].ToString()),
                                WorkYear = Convert.ToString(dr["wrkyear"].ToString()),
                                slno = Convert.ToInt32(dr["slno"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new DepartmentwiseempdetailLists
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