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
    public class LocWiseAttDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getlocwiseattdetails")]
        public HttpResponseMessage GetDetails(Listlocwiseattdetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<locwiseattdetailsLists> alldcr = new List<locwiseattdetailsLists>();
                    List<locwiseattdetailsList> alldcr1 = new List<locwiseattdetailsList>();

                    var dr = g1.return_dr("[hrm].[locawiseattendancedetails]'" + ula.CIN + "','" + ula.Date + "','" + ula.LocationId + "','" + ula.type + "'");
                    //var dr = g1.return_dr("[locawiseattendancedetails]'" + ula.CIN + "','" + ula.Date + "','" + ula.LocationId + "','" + ula.type + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new locwiseattdetailsList
                            {

                                EmployeeCode = Convert.ToString(dr["empcode"].ToString()),
                                Employeeslno = Convert.ToString(dr["empslno"].ToString()),
                                EmployeeName = Convert.ToString(dr["empname"].ToString()),
                                Departmentname = Convert.ToString(dr["dept"].ToString()),
                                DesignationName = Convert.ToString(dr["desig"].ToString()),
                                Location = Convert.ToString(dr["loc"].ToString()),
                                SubLocation = Convert.ToString(dr["subloc"].ToString()),

                                Branchname = Convert.ToString(dr["branch"].ToString()),
                                DOB = Convert.ToString(dr["dob"].ToString()),
                                Address = Convert.ToString(dr["address"].ToString()),
                                Intime = Convert.ToString(dr["InTime"].ToString()),
                                Out = Convert.ToString(dr["OutTime"].ToString()),
                                Dueration = Convert.ToString(dr["dueration"].ToString()),
                                MobileNumber = Convert.ToString(dr["contactno"].ToString()),
                                JoinDate = Convert.ToString(dr["joindt"].ToString()),
                                WorkYear = Convert.ToString(dr["wrkyear"].ToString()),
                                Fatehr = Convert.ToString(dr["FatherName"].ToString()),
                                Mother = Convert.ToString(dr["MotherName"].ToString()),
                                Email = Convert.ToString(dr["email"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new locwiseattdetailsLists
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