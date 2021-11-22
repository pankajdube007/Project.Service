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
    public class pendingOrderDivisionWiseExController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getPendingOrderDivisionWiseExcutive")]
        public HttpResponseMessage GetDetails(PendingOrderDivisionExAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<PendingOrderDivisionExs> alldcr = new List<PendingOrderDivisionExs>();
                    List<PendingOrderDivisionEx> alldcr1 = new List<PendingOrderDivisionEx>();
                    List<PendingOrderDivisionExDetails> PendingOrderDivisionDetails = new List<PendingOrderDivisionExDetails>();
                    List<PendingOrderDivisionExTotal> PendingOrderDivisionTotal = new List<PendingOrderDivisionExTotal>();

                    DataTable dr = g1.return_dt("App_PendingOrderDivisionWiseExcutive " + ula.ExId + ",'" + ula.CIN + "'," + Convert.ToBoolean(ula.Hierarchy) + "," + ula.index + "," + ula.count);

                    if (!dr.Columns.Contains("WIRING DEVICES"))
                    {
                        dr.Columns.Add("WIRING DEVICES", typeof(string));
                        dr.Columns["WIRING DEVICES"].Expression = "'0.00'";
                    }
                    if (!dr.Columns.Contains("LIGHTS"))
                    {
                        dr.Columns.Add("LIGHTS", typeof(string));
                        dr.Columns["LIGHTS"].Expression = "'0.00'";
                    }
                    if (!dr.Columns.Contains("WIRE & CABLE"))
                    {
                        dr.Columns.Add("WIRE & CABLE", typeof(string));
                        dr.Columns["WIRE & CABLE"].Expression = "'0.00'";
                    }
                    if (!dr.Columns.Contains("PIPES & FITTINGS"))
                    {
                        dr.Columns.Add("PIPES & FITTINGS", typeof(string));
                        dr.Columns["PIPES & FITTINGS"].Expression = "'0.00'";
                    }
                    if (!dr.Columns.Contains("MCB & DBS"))
                    {
                        dr.Columns.Add("MCB & DBS", typeof(string));
                        dr.Columns["MCB & DBS"].Expression = "'0.00'";
                    }

                    if (!dr.Columns.Contains("FAN"))
                    {
                        dr.Columns.Add("FAN", typeof(string));
                        dr.Columns["FAN"].Expression = "'0.00'";
                    }

                    bool more = false;
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

                        decimal total = 0;
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            PendingOrderDivisionDetails.Add(new PendingOrderDivisionExDetails
                            {
                                partynm = Convert.ToString(dr.Rows[i]["partynm"]),
                                exnm = Convert.ToString(dr.Rows[i]["salesexname"]),
                                WIRINGDEVICES = Convert.ToString(dr.Rows[i]["WIRING DEVICES"]),
                                LIGHTS = Convert.ToString(dr.Rows[i]["LIGHTS"]),
                                WIRECABLE = Convert.ToString(dr.Rows[i]["WIRE & CABLE"]),
                                PIPESFITTINGS = Convert.ToString(dr.Rows[i]["PIPES & FITTINGS"]),
                                MCBDBS = Convert.ToString(dr.Rows[i]["MCB & DBS"]),
                                FAN = Convert.ToString(dr.Rows[i]["FAN"]),
                                // pending = Convert.ToString(dr.Rows[i]["pendingamt"])
                            });

                            total = total
                                + Convert.ToDecimal(dr.Rows[i]["WIRING DEVICES"])
                                + Convert.ToDecimal(dr.Rows[i]["LIGHTS"])
                                + Convert.ToDecimal(dr.Rows[i]["WIRE & CABLE"])
                                + Convert.ToDecimal(dr.Rows[i]["PIPES & FITTINGS"])
                                + Convert.ToDecimal(dr.Rows[i]["MCB & DBS"])
                                + Convert.ToDecimal(dr.Rows[i]["FAN"]);
                        }

                        PendingOrderDivisionTotal.Add(new PendingOrderDivisionExTotal
                        {
                            // totalsale = dr1.Compute("Sum(BalAmt)", "").ToString()
                            pendingtotal = Math.Round(total, 2).ToString()
                        });

                        alldcr1.Add(new PendingOrderDivisionEx
                        {
                            pendingdetails = PendingOrderDivisionDetails,
                            pendingtotal = PendingOrderDivisionTotal,
                            ismore = more
                        });

                        g1.close_connection();
                        alldcr.Add(new PendingOrderDivisionExs
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