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
    public class AutomationLeadGenerationShowroomQuotationInsertController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AutomationLeadGenerationShowroomQuotationInsert")]
        public HttpResponseMessage GetDetails(GetAddofAutomationLeadGenerationShowroomQuotation ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            try
            {
                string data1;

                string ordermsg = string.Empty;

                List<GetAddAutomationLeadGenerationShowroomQuotationLists> alldcr = new List<GetAddAutomationLeadGenerationShowroomQuotationLists>();
                List<GetAddAutomationLeadGenerationShowroomQuotationList> alldcr1 = new List<GetAddAutomationLeadGenerationShowroomQuotationList>();
                var dr = g2.return_dt("Automation_Lead_GenerationShowroomQuotationInsert '"
                    + ula.CIN + "',"
                    + ula.HeadSlNo + ",'"
                    + ula.OrderDetails.ToString() + "'");

                g2.close_connection();

                if (dr.Rows.Count > 0)
                {
                    alldcr1.Add(new GetAddAutomationLeadGenerationShowroomQuotationList
                    {
                        //type = dr.Rows[0]["type1"].ToString(),
                        output = "Data Sucessfully inserted" // dr.Rows[0]["msg"].ToString().TrimEnd(','),
                    });

                    if (dr.Rows.Count > 0)
                    {
                        alldcr.Add(new GetAddAutomationLeadGenerationShowroomQuotationLists
                        {
                            result = true,
                            message = "",
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
                        alldcr.Add(new GetAddAutomationLeadGenerationShowroomQuotationLists
                        {
                            result = false,
                            message = "",
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                else
                {
                    g2.close_connection();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Data not Inserted"), Encoding.UTF8, "application/json");

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
    }
}