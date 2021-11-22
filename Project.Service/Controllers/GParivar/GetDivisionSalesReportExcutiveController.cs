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
    public class GetDivisionSalesReportExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDivisionSalesReportExcutive")]
        public HttpResponseMessage GetDetails(DivisionSalesReportExAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    decimal stock = 0;
                    List<DivisionSalesReportExs> alldcr = new List<DivisionSalesReportExs>();
                    List<DivisionSalesReportEx> alldcr1 = new List<DivisionSalesReportEx>();
                    List<DivisionSalesReportExall> DivisionSalesAll = new List<DivisionSalesReportExall>();
                    var dr = g1.return_dr("App_getDivisionSalesReportExcutive " + ula.ExId + ",'" + ula.CIN + "','" + ula.FinYear + "'," + ula.ReportType + "," + ula.ReportValue + "," + Convert.ToBoolean(ula.Hierarchy));
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DivisionSalesReportEx
                            {
                                partynm = Convert.ToString(dr["partynm"].ToString()),
                                exnm = Convert.ToString(dr["salesexname"].ToString()),
                                Division = Convert.ToString(dr["divisionnm"].ToString()),
                                //  AmountinLk = Convert.ToString(Math.Round(Convert.ToDecimal(dr["withouttax"]) / 100000, 2)),
                                Amount = Convert.ToString(Math.Round(Convert.ToDecimal(dr["withouttax"]), 2)),
                            });
                            stock += Convert.ToDecimal(dr["withouttax"]);
                        }

                        DivisionSalesAll.Add(new DivisionSalesReportExall
                        {
                            DivisionSalesReport = alldcr1,
                            //   TotalAmtinlk = Convert.ToString(Math.Round(stock / 100000, 2)),
                            TotalAmt = Convert.ToString(Math.Round(stock, 2))
                        });
                        g1.close_connection();
                        alldcr.Add(new DivisionSalesReportExs
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