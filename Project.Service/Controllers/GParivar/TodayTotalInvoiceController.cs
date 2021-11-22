using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class TodayTotalInvoiceController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getTotalInvoiceToday")]
        public HttpResponseMessage GetDetails()
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<TotalTodayInvoices> alldcr = new List<TotalTodayInvoices>();
                List<TotalTodayInvoice> alldcr1 = new List<TotalTodayInvoice>();
                var dr = g1.return_dt("TodaytotalInvoice ");

                if (dr.Rows.Count > 0)
                {
                    for (int i = 0; i < dr.Rows.Count; i++)
                    {
                        alldcr1.Add(new TotalTodayInvoice
                        {
                            totalamt = dr.Rows[i]["totalamt"].ToString(),
                        });
                    }
                    g1.close_connection();
                    alldcr.Add(new TotalTodayInvoices
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
    }
}