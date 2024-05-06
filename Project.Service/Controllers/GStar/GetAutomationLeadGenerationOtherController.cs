using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.GStar;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class GetAutomationLeadGenerationOtherController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetAutomationLeadGenerationOther")]
        public HttpResponseMessage GetDetails(GetAutomationLeadGenerationOther ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0 || ula.CIN != null) // (ula.MobileNo != null)
            {
                try
                {
                    string data1;

                    List<GetListAutomationLeadGenerationOthers> alldcr = new List<GetListAutomationLeadGenerationOthers>();
                    List<GetListAutomationLeadGenerationOther> alldcr1 = new List<GetListAutomationLeadGenerationOther>();
                    var dr = g1.return_dr("dbo.GetAutomationLeadGenerationother_API '" + ula.MobileNo + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetListAutomationLeadGenerationOther
                            {
                                slno = Convert.ToString(dr["slno"].ToString()),
                                OtherNo = Convert.ToString(dr["other_No"].ToString()),
                                Other_Name = Convert.ToString(dr["other_Name"].ToString()),
                                compname = Convert.ToString(dr["compname"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetListAutomationLeadGenerationOthers
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