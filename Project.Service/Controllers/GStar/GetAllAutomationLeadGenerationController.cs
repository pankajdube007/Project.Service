using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class GetAllAutomationLeadGenerationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetAllAutomationLeadGeneration")]
        public HttpResponseMessage GetDetails(GetListofAutomationLeadGeneration ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                
                try
                {
                    string data1;

                    List<GetAutomationLeadGenerationLists> alldcr = new List<GetAutomationLeadGenerationLists>();
                    List<GetAutomationLeadGenerationList> alldcr1 = new List<GetAutomationLeadGenerationList>();
                    var dr = g1.return_dr("dbo.GetAllAutomationLeadGeneration_GParivar_Gstar_API '" + ula.CIN + "', '" + ula.Project_name + "','" + ula.FromDate + "','" + ula.ToDate + "','" + ula.SearchData + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetAutomationLeadGenerationList
                            {
                                SlNo = Convert.ToString(dr["SlNo"].ToString()),
                                Reference_NO = Convert.ToString(dr["Reference_NO"].ToString()),
                                Cust_Mob_No = Convert.ToString(dr["Cust_Mob_No"].ToString()),
                                Cust_Name = Convert.ToString(dr["Cust_Name"].ToString()),
                                FullAddress = Convert.ToString(dr["FullAddress"].ToString()),
                                ApprovalStatus = Convert.ToString(dr["ApprovalStatus"].ToString()),
                                RequestedDate = Convert.ToString(dr["RequestedDate"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetAutomationLeadGenerationLists
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