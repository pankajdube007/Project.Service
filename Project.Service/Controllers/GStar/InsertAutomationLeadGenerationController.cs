using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
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
            HttpResponseMessage response = null;

            if (ula.Cin != "")
            {
                try
                {
                    string data1;

                    List<AddAutomationLeadGenerations> alldcr = new List<AddAutomationLeadGenerations>();
                    List<AddAutomationLeadGeneration> alldcr1 = new List<AddAutomationLeadGeneration>();
                    //AutomationLeadGenerationAdd_GParivar_Gstar_API_New
                    //AutomationLeadGenerationAdd_GParivar_Gstar_API_New_Updated
                    var result = g2.ExecDB("dbo.AutomationLeadGenerationAdd_GParivar_Gstar_API_New_Updated '" + ula.Cin + "','" + ula.Purpose + "','" + ula.CustomerMobileNo + "','" + ula.CustomerName + "','" + ula.EmailID + "','" + ula.AddressLine1 + "','" + ula.AddressLine2 + "'," + ula.Pincode + ",'" + ula.StateID + "','" + ula.DistrictID + "','" + ula.City + "','" + ula.AutomationCategoryID + "','" + ula.ItemID + "','" + ula.IsInvloveArchitect + "','" + ula.ArchitectMobileNo + "', '" + ula.ArchitectName + "','" + ula.Architech_CompanyName + "', '" + ula.Available_dt + "','" + ula.Available_time + "','" + ula.Remark + "','" + ula.Project_name + "','" + ula.ArchitectID + "'");

                    switch (result)
                    {
                        case 1:
                            alldcr1.Add(new AddAutomationLeadGeneration
                            {
                                output = "Data Successfully inserted"
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
                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                            break;

                        case 0:
                            alldcr1.Add(new AddAutomationLeadGeneration
                            {
                                output = "" // "Automation Lead Generation with Mobile No " + ula.CustomerMobileNo + " already exists ! Please Close or Reject the Previously generated automation leads to generate the New Leads."
                            });

                            g2.close_connection();
                            alldcr.Add(new AddAutomationLeadGenerations
                            {
                                result = false,
                                message = "Automation Lead Generation with Mobile No " + ula.CustomerMobileNo + " already exists ! Please Close or Reject the Previously generated automation leads to generate the New Leads.",
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                            break;

                        default:
                            g2.close_connection();
                            response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Automation Lead Generation Not Created!!!!!!!!"), Encoding.UTF8, "application/json");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");
            }

            return response;
        }
    }
}
