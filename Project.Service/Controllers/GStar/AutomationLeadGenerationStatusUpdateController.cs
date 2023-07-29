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
    public class AutomationLeadGenerationStatusUpdateController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AutomationLeadGenerationStatusUpdate")]
        public HttpResponseMessage GetDetails(InsertAutomationLeadGenerationStatusUpdate ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.Cin != "")
            {
                try
                {
                    string data1;

                    List<AddAutomationLeadGenerationStatusUpdates> alldcr = new List<AddAutomationLeadGenerationStatusUpdates>();
                    List<AddAutomationLeadGenerationStatusUpdate> alldcr1 = new List<AddAutomationLeadGenerationStatusUpdate>();
                    
                    var dr = g2.return_dr("dbo.AutomationLeadGenerationStatusUpdate_GParivar_Gstar_API '" + ula.Cin + "','" + ula.Status + "','" + ula.Remark + "','" + ula.Project_name + "','" + ula.FollowUpDate + "','" + ula.SlNo + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddAutomationLeadGenerationStatusUpdate
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddAutomationLeadGenerationStatusUpdates
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message.ToString()), Encoding.UTF8, "application/json");

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