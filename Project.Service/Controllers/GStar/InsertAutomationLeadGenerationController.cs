using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GParivar
{
    public class InsertAutomationLeadGenerationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AutomationLeadGenerationAdd")]
        public HttpResponseMessage GetDetails(InsertAutomationLeadGeneration ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.Cin != "")
            {
                try
                {
                    string data1;

                    List<AddAutomationLeadGenerations> alldcr = new List<AddAutomationLeadGenerations>();
                    List<AddAutomationLeadGeneration> alldcr1 = new List<AddAutomationLeadGeneration>();
                    //var dr = g2.return_dr("appdisappmprlist '" + ula.MprId + "','" + ula.Cin + "','" + ula.Type + "','" + ula.Remark + "'");

                       var dr = g2.return_dr("dbo.AutomationLeadGenerationAdd_GParivar_Gstar_API'" + ula.Cin + "','" + ula.CustomerMobileNo + "','" + ula.CustomerName + "','" + ula.Pincode + "','" + ula.AddressLine1 + "','" + ula.AddressLine2 + "','" + ula.StateID + "','" + ula.DistrictID + "','" + ula.City + "','" + ula.AutomationCategoryID + "','" + ula.IsInvloveArchitect + "','" + ula.ArchitectName + "','" + ula.ArchitectMobileNo + "','" + ula.Remark + "','" + ula.Available_dt + "','" + ula.Available_time + "','" + ula.CategoryType + "'");

                    //var dr = g2.return_dr("dbo.AutomationLeadGenerationAdd_GParivar_Gstar_API'" + ula.Cin + "','" + ula.CustomerMobileNo + "','" + ula.CustomerName + "','" + ula.Pincode + "','" + ula.AddressLine1 + "','" + ula.AddressLine2 + "','" + ula.StateID + "','" + ula.DistrictID + "','" + ula.City + "','" + ula.AutomationCategoryID + "'");
                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddAutomationLeadGeneration
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddAutomationLeadGenerations
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Automation Lead Generation Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"+ex.Message.ToString()), Encoding.UTF8, "application/json");

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