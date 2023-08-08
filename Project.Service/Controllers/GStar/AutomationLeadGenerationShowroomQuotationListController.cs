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
    public class AutomationLeadGenerationShowroomQuotationListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetShowroomQuotationListAutomationLeadGeneration")]
        public HttpResponseMessage GetDetails(GetListOfAutomationLeadGenerationShowroomQuotation ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                
                try
                {
                    string data1;

                    List<GetAutomationLeadGenerationShowroomQuotationLists> alldcr = new List<GetAutomationLeadGenerationShowroomQuotationLists>();
                    List<GetAutomationLeadGenerationShowroomQuotationList> alldcr1 = new List<GetAutomationLeadGenerationShowroomQuotationList>();
                    var dr = g1.return_dr("dbo.AutomationLeadGenerationShowroomQuotation_GParivar_Gstar_API '" + ula.SearchItem + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetAutomationLeadGenerationShowroomQuotationList
                            {
                                slno = Convert.ToString(dr["slno"].ToString()),
                                ProductCode = Convert.ToString(dr["ProductCode"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                divisionid = Convert.ToString(dr["divisionid"].ToString()),
                                UnitName = Convert.ToString(dr["UnitName"].ToString()),
                                unitid = Convert.ToString(dr["unitid"].ToString()),
                                Rate = Convert.ToString(dr["Rate"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetAutomationLeadGenerationShowroomQuotationLists
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