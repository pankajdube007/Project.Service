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
    public class TotalPaymentPartyWiseManagementController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getTotalPaymentDivisionWiseManagement")]
        public HttpResponseMessage GetDetails(TotalPaymentDivisionWiseManagementAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.BranchId != 0 && ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<TotalPaymentDivisionWiseManagements> alldcr = new List<TotalPaymentDivisionWiseManagements>();
                    List<TotalPaymentDivisionWiseManagementFinal> alldcr1 = new List<TotalPaymentDivisionWiseManagementFinal>();
                    List<TotalPaymentDivisionWiseManagement> paymentDivisionDetails = new List<TotalPaymentDivisionWiseManagement>();
                    TotalPaymentDivisionWiseManagementTotal total = new TotalPaymentDivisionWiseManagementTotal();

                    DataTable dr = g1.return_dt("PaymentReportManagementBranchDivision '" + ula.FromDate + "','" + ula.ToDate + "'," + ula.BranchId + "," + ula.index + "," + ula.count+",'"+ula.CIN+ "','" + ula.Category + "'");
                    DataTable dr1 = g1.return_dt("PaymentReportManagementBranchDivision '" + ula.FromDate + "','" + ula.ToDate + "'," + ula.BranchId + ",0," + 99999+",'" + ula.CIN + "','" + ula.Category + "'");

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
                    
                    if (!dr.Columns.Contains("Fan"))
                    {
                        dr.Columns.Add("Fan", typeof(string));
                        dr.Columns["Fan"].Expression = "'0.00'";
                    }
                    if (!dr.Columns.Contains("HEALTHCARE"))
                    {
                        dr.Columns.Add("HEALTHCARE", typeof(string));
                        dr.Columns["HEALTHCARE"].Expression = "'0.00'";
                    }






                    if (!dr1.Columns.Contains("WIRING DEVICES"))
                    {
                        dr1.Columns.Add("WIRING DEVICES", typeof(string));
                        dr1.Columns["WIRING DEVICES"].Expression = "'0.00'";
                    }
                    if (!dr1.Columns.Contains("LIGHTS"))
                    {
                        dr1.Columns.Add("LIGHTS", typeof(string));
                        dr1.Columns["LIGHTS"].Expression = "'0.00'";
                    }
                    if (!dr1.Columns.Contains("WIRE & CABLE"))
                    {
                        dr1.Columns.Add("WIRE & CABLE", typeof(string));
                        dr1.Columns["WIRE & CABLE"].Expression = "'0.00'";
                    }
                    if (!dr1.Columns.Contains("PIPES & FITTINGS"))
                    {
                        dr1.Columns.Add("PIPES & FITTINGS", typeof(string));
                        dr1.Columns["PIPES & FITTINGS"].Expression = "'0.00'";
                    }
                    if (!dr1.Columns.Contains("MCB & DBS"))
                    {
                        dr1.Columns.Add("MCB & DBS", typeof(string));
                        dr1.Columns["MCB & DBS"].Expression = "'0.00'";
                    }
                    if (!dr1.Columns.Contains("Fan"))
                    {
                        dr1.Columns.Add("Fan", typeof(string));
                        dr1.Columns["Fan"].Expression = "'0.00'";
                    }
                    if (!dr1.Columns.Contains("HEALTHCARE"))
                    {
                        dr1.Columns.Add("HEALTHCARE", typeof(string));
                        dr1.Columns["HEALTHCARE"].Expression = "'0.00'";
                    }



                    bool more = false;
                    decimal wiringdevicetotal = 0;
                    decimal lightetotal = 0;
                    decimal wireandcabletotal = 0;
                    decimal pipesandfittingtotal = 0;
                    decimal mcbanddbtotal = 0;
                    decimal fantotal = 0;
                    decimal HEALTHCAREtotal = 0;

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
                            paymentDivisionDetails.Add(new TotalPaymentDivisionWiseManagement
                            {
                                partynm = Convert.ToString(dr.Rows[i]["dealnm"]),
                                cin = Convert.ToString(dr.Rows[i]["cinnum"]),
                                partystatus = Convert.ToString(dr.Rows[i]["workstatus"]),
                                exnm = Convert.ToString(dr.Rows[i]["salesexnm"]),
                                WIRINGDEVICES = Convert.ToString(dr.Rows[i]["WIRING DEVICES"]),
                                LIGHTS = Convert.ToString(dr.Rows[i]["LIGHTS"]),
                                WIRECABLE = Convert.ToString(dr.Rows[i]["WIRE & CABLE"]),
                                PIPESFITTINGS = Convert.ToString(dr.Rows[i]["PIPES & FITTINGS"]),
                                MCBDBS = Convert.ToString(dr.Rows[i]["MCB & DBS"]),
                                FAN = Convert.ToString(dr.Rows[i]["fan"]),
                                 HEALTHCARE = Convert.ToString(dr.Rows[i]["HEALTHCARE"]),
                                // pending = Convert.ToString(dr.Rows[i]["pendingamt"])
                            });
                        }

                        for (int i = 0; i < dr1.Rows.Count; i++)
                        {

                            wiringdevicetotal = wireandcabletotal + Convert.ToDecimal(dr1.Rows[i]["WIRING DEVICES"]);
                            lightetotal = lightetotal + Convert.ToDecimal(dr1.Rows[i]["LIGHTS"]);
                            wireandcabletotal = wireandcabletotal + Convert.ToDecimal(dr1.Rows[i]["WIRE & CABLE"]);
                            pipesandfittingtotal = pipesandfittingtotal + Convert.ToDecimal(dr1.Rows[i]["PIPES & FITTINGS"]);
                            mcbanddbtotal = mcbanddbtotal + Convert.ToDecimal(dr1.Rows[i]["MCB & DBS"]);
                            fantotal = fantotal + Convert.ToDecimal(dr1.Rows[i]["fan"]);
                            HEALTHCAREtotal = HEALTHCAREtotal + Convert.ToDecimal(dr1.Rows[i]["HEALTHCARE"]);
                        }


                        total.wiringdevicetotal = Convert.ToString(wiringdevicetotal);
                        total.lightetotal = Convert.ToString(lightetotal);
                        total.wireandcabletotal = Convert.ToString(wireandcabletotal);
                        total.pipesandfittingtotal = Convert.ToString(pipesandfittingtotal);
                        total.mcbanddbtotal = Convert.ToString(mcbanddbtotal);
                        total.fantotal = Convert.ToString(fantotal);
                        total.HEALTHCAREtotal = Convert.ToString(HEALTHCAREtotal);

                        alldcr1.Add(new TotalPaymentDivisionWiseManagementFinal
                        {
                            paymentdetails = paymentDivisionDetails,
                            PaymentdetailsTotal = total,
                            ismore = more
                        });

                        g1.close_connection();
                        alldcr.Add(new TotalPaymentDivisionWiseManagements
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