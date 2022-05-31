using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GParivar
{
    public class GetAutomationLeadGenerationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetAutomationLeadGeneration")]
        public HttpResponseMessage GetDetails(GetAutomationLeadGeneration ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetListAutomationLeadGenerations> alldcr = new List<GetListAutomationLeadGenerations>();
                    List<GetListAutomationLeadGeneration> alldcr1 = new List<GetListAutomationLeadGeneration>();
                    var dr = g1.return_dr("dbo.GetAutomationLeadGeneration_GParivar_Gstar_API '" + ula.CIN + "', '" + ula.CategoryType + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetListAutomationLeadGeneration
                            {
                                Cust_Mob_No = Convert.ToString(dr["Cust_Mob_No"].ToString()),
                                Cust_Name = Convert.ToString(dr["Cust_Name"].ToString()),
                                Pincode = Convert.ToString(dr["Pincode"].ToString()),
                                Add_Line_1 = Convert.ToString(dr["Add_Line_1"].ToString()),
                                Add_Line_2 = Convert.ToString(dr["Add_Line_2"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                Distrctnm = Convert.ToString(dr["Distrctnm"].ToString()),
                                City_ID = Convert.ToString(dr["City_ID"].ToString()),
                                categorynm = Convert.ToString(dr["categorynm"].ToString()),
                                Involve_Architech = Convert.ToString(dr["Involve_Architech"].ToString()),
                                Architech_No = Convert.ToString(dr["Architech_No"].ToString()),
                                Architech_Name = Convert.ToString(dr["Architech_Name"].ToString()),
                                Remark = Convert.ToString(dr["Remark"].ToString()),
                                Project_name = Convert.ToString(dr["Project_name"].ToString()),
                                ApprovalStatus = Convert.ToString(dr["ApprovalStatus"].ToString()),
                                Available_dt = Convert.ToString(dr["Available_dt"].ToString()),
                                Available_time = Convert.ToString(dr["Available_time"].ToString()),
                                CategoryType = Convert.ToString(dr["CategoryType"].ToString()),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetListAutomationLeadGenerations
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