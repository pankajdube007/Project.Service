using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class AgingExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSalesExcutiveAging")]
        public HttpResponseMessage GetDetails(ListsofAgingEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    string partyid = string.Empty;

                    List<AgingExs> alldcr = new List<AgingExs>();
                    List<AgingEx> alldcr1 = new List<AgingEx>();
                    List<AgingExDeatail> AgingDeatail = new List<AgingExDeatail>();
                    List<AgingExurl> Agingurl = new List<AgingExurl>();
                    var dr = g1.return_dt("App_AgingReportForExecutive " + ula.ExId + ",'" + ula.CIN + "'," + Convert.ToBoolean(ula.Hierarchy) + "," + ula.index + "," + ula.count);
                    var dr1 = g1.return_dt("App_AgingReportForExecutive " + ula.ExId + ",'" + ula.CIN + "'," + Convert.ToBoolean(ula.Hierarchy) + "," + 0 + "," + 999999);

                    if (!dr.Columns.Contains("0-30"))
                    {
                        dr.Columns.Add("0-30", typeof(string));
                        dr.Columns["0-30"].Expression = "'0'";
                    }
                    if (!dr.Columns.Contains("31-60"))
                    {
                        dr.Columns.Add("31-60", typeof(string));
                        dr.Columns["31-60"].Expression = "'0'";
                    }
                    if (!dr.Columns.Contains("61-90"))
                    {
                        dr.Columns.Add("61-90", typeof(string));
                        dr.Columns["61-90"].Expression = "'0'";
                    }
                    if (!dr.Columns.Contains("91-Above"))
                    {
                        dr.Columns.Add("91-Above", typeof(string));
                        dr.Columns["91-Above"].Expression = "'0'";
                    }

                    if (!dr1.Columns.Contains("0-30"))
                    {
                        dr1.Columns.Add("0-30", typeof(string));
                        dr1.Columns["0-30"].Expression = "'0'";
                    }
                    if (!dr1.Columns.Contains("31-60"))
                    {
                        dr1.Columns.Add("31-60", typeof(string));
                        dr1.Columns["31-60"].Expression = "'0'";
                    }
                    if (!dr1.Columns.Contains("61-90"))
                    {
                        dr1.Columns.Add("61-90", typeof(string));
                        dr1.Columns["61-90"].Expression = "'0'";
                    }
                    if (!dr1.Columns.Contains("91-Above"))
                    {
                        dr1.Columns.Add("91-Above", typeof(string));
                        dr1.Columns["91-Above"].Expression = "'0'";
                    }

                    bool more = false;
                    decimal zeroto30total = 0, thirtyoneto60total = 0, sixtyoneto90total = 0, nintyonetoabovetotal = 0;
                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.count + ula.index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }

                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            AgingDeatail.Add(new AgingExDeatail
                            {
                                partynam = Convert.ToString(dr.Rows[i]["partyname"]),
                                higherdays = ((DateTime.Now.Date - Convert.ToDateTime(dr.Rows[i]["highdate"]).Date).TotalDays).ToString(),
                                exnm = Convert.ToString(dr.Rows[i]["salesexname"]),
                                zeroto30 = Math.Round(Convert.ToDecimal(dr.Rows[i]["0-30"]), 0).ToString(),
                                thirtyoneto60 = Math.Round(Convert.ToDecimal(dr.Rows[i]["31-60"]), 0).ToString(),
                                sixtyoneto90 = Math.Round(Convert.ToDecimal(dr.Rows[i]["61-90"]), 0).ToString(),
                                nintyonetoabove = Math.Round(Convert.ToDecimal(dr.Rows[i]["91-Above"]), 0).ToString(),

                                //  Amount = Convert.ToString(dr.Rows[i]["outstandingamt"])
                            });
                            partyid = partyid + Convert.ToString(dr.Rows[i]["partyid"]) + ",";
                        }

                        for (int i = 0; i < dr1.Rows.Count; i++)
                        {
                            zeroto30total = zeroto30total + Convert.ToDecimal(dr1.Rows[i]["0-30"]);
                            thirtyoneto60total = thirtyoneto60total + Convert.ToDecimal(dr1.Rows[i]["31-60"]);
                            sixtyoneto90total = sixtyoneto90total + Convert.ToDecimal(dr1.Rows[i]["61-90"]);
                            nintyonetoabovetotal = nintyonetoabovetotal + Convert.ToDecimal(dr1.Rows[i]["91-Above"]);
                        }

                        Agingurl.Add(new AgingExurl
                        {
                            url = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "Outstanding-Report-View.aspx?Receiptwith=Default&partyid=" + partyid.TrimEnd(',')
                            + "&fromdate=01/04/2015&todate=" + DateTime.Now.ToString("dd/MM/yyyy") + "&branchid=" + Convert.ToString(dr.Rows[0]["typecat"]),
                            zeroto30total = Math.Round(zeroto30total, 0).ToString(),
                            thirtyoneto60total = Math.Round(thirtyoneto60total, 0).ToString(),
                            sixtyoneto90total = Math.Round(sixtyoneto90total, 0).ToString(),
                            nintyonetoabovetotal = Math.Round(nintyonetoabovetotal, 0).ToString(),
                            finaltotal = Math.Round(zeroto30total+ thirtyoneto60total + sixtyoneto90total + nintyonetoabovetotal, 0).ToString(),
                        });

                        alldcr1.Add(new AgingEx
                        {
                            AgingDetails = AgingDeatail,
                            Agingurls = Agingurl,
                            ismore = more
                        });

                        g1.close_connection();
                        alldcr.Add(new AgingExs
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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