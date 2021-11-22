using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class MprListCloseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getmprcloselist")]
        public HttpResponseMessage GetDetails(ListMprlistclose ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<Mprlistcloses> alldcr = new List<Mprlistcloses>();
                    List<Mprlistclose> alldcr1 = new List<Mprlistclose>();
                    //var dr = g1.return_dr("ManagementMPRSelectList '" + ula.CIN + "','" + ula.Category + "'");
                    var dr = g1.return_dr("hrm.ManagementMPRSelectListclose '" + ula.CIN + "','" + ula.Category + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new Mprlistclose
                            {

                                MPRid = Convert.ToInt32(dr["MPR_id"]),
                                MPRRequestNO = Convert.ToString(dr["MPR_RequestNO"].ToString()),
                                Requestedby = Convert.ToString(dr["Approved1ReportingById"].ToString()),
                                RequestedDesignation = Convert.ToString(dr["RequestedDesignation"].ToString()),
                                RequestedDate = Convert.ToString(dr["RequestedDate"]),
                                PositionTitle = Convert.ToString(dr["PositionTitle"].ToString()),
                                NoPosition = Convert.ToString(dr["NoPosition"].ToString()),
                                Budget = Convert.ToString(dr["Budget"].ToString()),
                                EmployeeType = Convert.ToString(dr["EmployeeType"]),
                                Location = Convert.ToString(dr["Location"].ToString()),
                                Description = Convert.ToString(dr["Description"].ToString()),
                                NatureOfRequest = Convert.ToString(dr["NatureOfRequest"].ToString()),
                                AgeRange = Convert.ToString(dr["AgeRange"]),
                                Status = Convert.ToString(dr["Status"].ToString()),
                                Gender = Convert.ToString(dr["Gender"].ToString()),
                                PreviousEmployeeName = Convert.ToString(dr["PreviousEmployeeName"].ToString()),
                                PreviousEmployeeDesignation = Convert.ToString(dr["PreviousEmployeeDesignation"]),
                                EducationRequirement = Convert.ToString(dr["EducationRequirement"].ToString()),
                                PreferedQualificationExprienece = Convert.ToString(dr["PreferedQualificationExprienece"].ToString()),
                                ReplacementReason = Convert.ToString(dr["ReplacementReason"].ToString()),
                                Department = Convert.ToString(dr["Department"]),
                                SubDaeprtment = Convert.ToString(dr["SubDaeprtment"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new Mprlistcloses
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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