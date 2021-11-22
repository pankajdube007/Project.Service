using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class YSAReportController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getYSAreport")]
        public HttpResponseMessage GetDetails(YSAReportAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<YSAReports> alldcr = new List<YSAReports>();
                    List<YSAReport> alldcr1 = new List<YSAReport>();
                    List<YSAReportDetails> YSAReportDetails = new List<YSAReportDetails>();
                    List<YSAReportTotal> YSAReportTotal = new List<YSAReportTotal>();

                    int m = DateTime.Now.Month;
                    int fy = 0;
                    int ty = 0;

                    if (m > 3)
                    {
                        fy = DateTime.Now.Year;
                        ty = DateTime.Now.Year + 1;
                    }
                    else
                    {
                        fy = DateTime.Now.Year - 1;
                        ty = DateTime.Now.Year;
                    }

                    // var dr = g1.return_dr("App_dealerSaleYsa " +fy+","+ty+",'FY "+fy+"-"+ty+"','"+ ula.CIN + "'");
                    DataTable dr = g1.return_dt("App_dealerSaleYsa " + fy + "," + ty + ",'FY " + fy + "-" + ty + "','" + ula.CIN + "'");

                    if (dr.Rows.Count > 0)
                    {
                        decimal total = 0;
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            YSAReportDetails.Add(new YSAReportDetails
                            {
                                groupnm = Convert.ToString(dr.Rows[i]["CatGroupNm"].ToString()),
                                ysa = Convert.ToString(dr.Rows[i]["ysa"].ToString()),
                                sale = Convert.ToString(dr.Rows[i]["totalsale"].ToString())
                            });

                            total = total + Convert.ToDecimal(dr.Rows[i]["totalsale"]);
                        }

                        YSAReportTotal.Add(new YSAReportTotal
                        {
                            totalsale = total.ToString()
                        });

                        alldcr1.Add(new YSAReport
                        {
                            ysadetails = YSAReportDetails,
                            ysatotal = YSAReportTotal
                        });

                        g1.close_connection();
                        alldcr.Add(new YSAReports
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