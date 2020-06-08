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
    public class OutstandingReportExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOutstandingReportExcutive")]
        public HttpResponseMessage GetDetails(ListsofOutstandingReportExAction ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<OutstandingReportExs> alldcr = new List<OutstandingReportExs>();
                    List<OutstandingReportEx> alldcr1 = new List<OutstandingReportEx>();
                    List<TotalOutstandingEx> TotalOutstanding = new List<TotalOutstandingEx>();
                    List<TotalDueEx> TotalDue = new List<TotalDueEx>();
                    List<OutstandingFinalEx> OutstandingFinal = new List<OutstandingFinalEx>();
                    var dr = g1.return_dt("App_OutstandingReportExcutive " + ula.ExId + ",'" + ula.CIN + "'," + ula.Division + "," + ula.OutstangingDays + "," + ula.Index + "," + ula.Count + "," + Convert.ToBoolean(ula.Hierarchy));
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
                            alldcr1.Add(new OutstandingReportEx
                            {
                                PartyName = Convert.ToString(dr.Rows[i]["displaynm"].ToString()),
                                InvoiceNo = Convert.ToString(dr.Rows[i]["invoiceno"].ToString()),
                                InvoiceDate = Convert.ToString(dr.Rows[i]["invoicedate"].ToString()),
                                DivisionName = Convert.ToString(dr.Rows[i]["divisionnm"].ToString()),
                                InvoiceAmt = Convert.ToString(dr.Rows[i]["finalamount"].ToString()),
                                OuststandingAmt = Convert.ToString(dr.Rows[i]["outstandingamt"].ToString()),
                                DueDays = Convert.ToString(dr.Rows[i]["duedays"].ToString()),
                            });
                        }

                        var dttotaldue = g1.return_dt("App_Outstanding '" + ula.CIN + "'," + "0");
                        TotalDue.Add(new TotalDueEx
                        {
                            Due = Convert.ToString(dttotaldue.Rows[0]["due"].ToString()),
                            OverDue = Convert.ToString(dttotaldue.Rows[0]["overdue"].ToString()),
                        });

                        var dttotalsum = g1.return_dt("App_OutstandingReportExcutive " + ula.ExId + ",'" + ula.CIN + "'," + ula.Division + "," + ula.OutstangingDays + "," + 0 + "," + 999999 + "," + Convert.ToBoolean(ula.Hierarchy));

                        TotalOutstanding.Add(new TotalOutstandingEx
                        {
                            InvoiceAmt = Convert.ToString(dttotalsum.Compute("SUM(finalamount)", string.Empty)),
                            OuststandingAmt = Convert.ToString(dttotalsum.Compute("SUM(outstandingamt)", string.Empty)),
                        });

                        OutstandingFinal.Add(new OutstandingFinalEx
                        {
                            outstandingdata = alldcr1,
                            totaloutstanding = TotalOutstanding,
                            Totaldueoverdue = TotalDue,
                            ismore = more
                        });

                        g1.close_connection();
                        alldcr.Add(new OutstandingReportExs
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