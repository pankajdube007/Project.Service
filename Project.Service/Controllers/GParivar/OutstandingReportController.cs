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
    public class OutstandingReportController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOutstandingReport")]
        public HttpResponseMessage GetDetails(ListsofOutstandingReportAction ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<OutstandingReports> alldcr = new List<OutstandingReports>();
                    List<OutstandingReport> alldcr1 = new List<OutstandingReport>();
                    List<TotalOutstanding> TotalOutstanding = new List<TotalOutstanding>();
                    List<TotalDue> TotalDue = new List<TotalDue>();
                    List<OutstandingFinal> OutstandingFinal = new List<OutstandingFinal>();
                    var dr = g1.return_dt("App_OutstandingReport '" + ula.CIN + "'," + ula.Division + "," + ula.OutstangingDays + "," + ula.Index + "," + ula.Count);
                    bool more = false;

                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.Count + ula.Index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }

                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new OutstandingReport
                            {
                                InvoiceId = Convert.ToInt32(dr.Rows[i]["invoiceid"].ToString()),
                                InvoiceNo = Convert.ToString(dr.Rows[i]["invoiceno"].ToString()),
                                InvoiceDate = Convert.ToString(dr.Rows[i]["invoicedate"].ToString()),
                                DivisionName = Convert.ToString(dr.Rows[i]["divisionnm"].ToString()),
                                InvoiceAmt = Convert.ToString(dr.Rows[i]["finalamount"].ToString()),
                                OuststandingAmt = Convert.ToString(dr.Rows[i]["outstandingamt"].ToString()),
                                DueDays = Convert.ToString(dr.Rows[i]["duedays"].ToString()),
                                cddate = Convert.ToString(dr.Rows[i]["cddate"].ToString()),
                                percent = Convert.ToString(dr.Rows[i]["per"].ToString()),
                                duestatus = Convert.ToString(dr.Rows[i]["dusstatus"].ToString()),
                                extraper = Convert.ToDecimal(dr.Rows[i]["extraper"].ToString()),
                            });
                        }

                        var dttotaldue = g1.return_dt("App_Outstanding '" + ula.CIN + "'," + "0");
                        TotalDue.Add(new TotalDue
                        {
                            Due = Convert.ToString(dttotaldue.Rows[0]["due"].ToString()),
                            OverDue = Convert.ToString(dttotaldue.Rows[0]["overdue"].ToString()),
                        });

                        var dttotalsum = g1.return_dt("App_OutstandingReport '" + ula.CIN + "'," + ula.Division + "," + ula.OutstangingDays + "," + 0 + "," + 999999);

                        TotalOutstanding.Add(new TotalOutstanding
                        {
                            InvoiceAmt = Convert.ToString(dttotalsum.Compute("SUM(finalamount)", string.Empty)),
                            OuststandingAmt = Convert.ToString(dttotalsum.Compute("SUM(outstandingamt)", string.Empty)),
                        });

                        OutstandingFinal.Add(new OutstandingFinal
                        {
                            outstandingdata = alldcr1,
                            totaloutstanding = TotalOutstanding,
                            Totaldueoverdue = TotalDue,
                            ismore = more
                        });

                        g1.close_connection();
                        alldcr.Add(new OutstandingReports
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = OutstandingFinal,
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