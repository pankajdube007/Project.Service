using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using static Project.Service.Models.InvoiceReportManagementStatewise;

namespace Project.Service.Controllers
{
    public class InvoiceReportManagementStatewiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetInvoiceReportManagementStatewise")]
        public HttpResponseMessage GetDetails(InputRequest inputRequest)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (inputRequest.CIN != "")
            {
                try
                {
                    string data1;

                    List<OutputResponse> alldcr = new List<OutputResponse>();
                    List<InvoiceReportManagementStatewises> alldcr1 = new List<InvoiceReportManagementStatewises>();

                    var dr = g1.return_dr(string.Format("exec TodayInvoiceReportManagementStatewise '{0}','{1}','{2}','{3}'", inputRequest.fromdate, inputRequest.todate,inputRequest.CIN,inputRequest.Category));
                   
                  
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new InvoiceReportManagementStatewises
                            {
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                saleamt = Convert.ToDecimal(dr["saleamt"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new OutputResponse
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
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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
