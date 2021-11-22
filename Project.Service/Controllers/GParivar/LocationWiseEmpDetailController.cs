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
    public class LocationWiseEmpDetailController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getlocationEmployeeListDetails")]
        public HttpResponseMessage GetDetails(ListLocationWiseEmpDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<LocationWiseEmpDetailsLists> alldcr = new List<LocationWiseEmpDetailsLists>();
                    List<LocationWiseEmpDetailsList> alldcr1 = new List<LocationWiseEmpDetailsList>();

                    var dr = g1.return_dr("[hrm].[EmployeeListDetailsDatalocwise]'" + ula.CIN + "','" + ula.LocationId + "','" + ula.Cat + "'," + ula.type + "");
                  

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new LocationWiseEmpDetailsList
                            {

                                EmployeeName = Convert.ToString(dr["EmployeeName"].ToString()),
                                EmployeeCode = Convert.ToString(dr["EmployeeCode"].ToString()),
                                Employeeslno= Convert.ToString(dr["empslno"].ToString()),
                                BranchName = Convert.ToString(dr["BranchName"].ToString()),
                                MobileNumber = Convert.ToString(dr["MobileNumber"].ToString()),
                                Department = Convert.ToString(dr["department"].ToString()),
                                JoinDate = Convert.ToString(dr["joindate"].ToString()),
                                WorkYear = Convert.ToString(dr["wrkyear"].ToString()),
                                Sublocation = Convert.ToString(dr["sublocation"].ToString()),
                                slno = Convert.ToInt32(dr["slno"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new LocationWiseEmpDetailsLists
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