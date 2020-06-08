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
    public class GetDivisionSalesReportController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDivisionSalesReport")]
        public HttpResponseMessage GetDetails(ListsofDivisionSalesReportAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;
                    decimal stock = 0;
                    List<DivisionSalesReports> alldcr = new List<DivisionSalesReports>();
                    List<DivisionSalesReport> alldcr1 = new List<DivisionSalesReport>();
                    List<DivisionSalesReportall> DivisionSalesAll = new List<DivisionSalesReportall>();
                    var dr = g1.return_dr("App_getDivisionSalesReport '" + ula.CIN + "','" + ula.FinYear + "'," + ula.ReportType + "," + ula.ReportValue);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DivisionSalesReport
                            {
                                Division = Convert.ToString(dr["divisionnm"].ToString()),
                                AmountinLk = Convert.ToString(Math.Round(Convert.ToDecimal(dr["withouttax"]) / 100000, 2)),
                                Amount = Convert.ToString(Math.Round(Convert.ToDecimal(dr["withouttax"]), 2)),
                            });
                            stock += Convert.ToDecimal(dr["withouttax"]);
                        }

                        DivisionSalesAll.Add(new DivisionSalesReportall
                        {
                            DivisionSalesReport = alldcr1,
                            TotalAmtinlk = Convert.ToString(Math.Round(stock / 100000, 2)),
                            TotalAmt = Convert.ToString(Math.Round(stock, 2))
                        });
                        g1.close_connection();
                        alldcr.Add(new DivisionSalesReports
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = DivisionSalesAll,
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