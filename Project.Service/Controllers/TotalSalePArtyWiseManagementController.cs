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
    public class TotalSalePArtyWiseManagementController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getTotalSaleDivisionWiseManagement")]
        public HttpResponseMessage GetDetails(TotalSaleDivisionWiseManagementAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.BranchId != 0 && ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<TotalSaleDivisionWiseManagements> alldcr = new List<TotalSaleDivisionWiseManagements>();
                    List<TotalSaleDivisionWiseManagementFinal> alldcr1 = new List<TotalSaleDivisionWiseManagementFinal>();
                    List<TotalSaleDivisionWiseManagement> saleDivisionDetails = new List<TotalSaleDivisionWiseManagement>();
                    TotalSaleDivisionWiseManagementTotal total = new TotalSaleDivisionWiseManagementTotal();

                    DataTable dr = g1.return_dt("InvoiceReportManagementBranchDivision '" + ula.FromDate + "','" + ula.ToDate + "'," + ula.BranchId + "," + ula.index + "," + ula.count+", '" + ula.CIN + "','" + ula.Category + "'");
                    DataTable dr1 = g1.return_dt("InvoiceReportManagementBranchDivision '" + ula.FromDate + "','" + ula.ToDate + "'," + ula.BranchId + ",0," + 99999+ ",'" + ula.CIN + "','" + ula.Category + "'");

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

                    bool more = false;
                    decimal wiringdevicetotal = 0;
                    decimal lightetotal = 0;
                    decimal wireandcabletotal = 0;
                    decimal pipesandfittingtotal = 0;
                    decimal mcbanddbtotal = 0;

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
                            saleDivisionDetails.Add(new TotalSaleDivisionWiseManagement
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
                                // pending = Convert.ToString(dr.Rows[i]["pendingamt"])
                            });
                        }

                        for (int i = 0; i < dr1.Rows.Count; i++)
                        {

                            wiringdevicetotal = wiringdevicetotal + Convert.ToDecimal(dr1.Rows[i]["WIRING DEVICES"]);
                            lightetotal = lightetotal+ Convert.ToDecimal(dr1.Rows[i]["LIGHTS"]);
                            wireandcabletotal = wireandcabletotal + Convert.ToDecimal(dr1.Rows[i]["WIRE & CABLE"]);
                            pipesandfittingtotal = pipesandfittingtotal + Convert.ToDecimal(dr1.Rows[i]["PIPES & FITTINGS"]);
                            mcbanddbtotal = mcbanddbtotal+Convert.ToDecimal(dr1.Rows[i]["MCB & DBS"]);
                        }


                        total.wiringdevicetotal = Convert.ToString(wiringdevicetotal);
                        total.lightetotal = Convert.ToString(lightetotal);
                        total.wireandcabletotal = Convert.ToString(wireandcabletotal);
                        total.pipesandfittingtotal = Convert.ToString(pipesandfittingtotal);
                        total.mcbanddbtotal = Convert.ToString(mcbanddbtotal);


                        alldcr1.Add(new TotalSaleDivisionWiseManagementFinal
                        {
                            saledetails = saleDivisionDetails,
                            saledetailsTotal=total,
                            ismore = more
                        });

                        g1.close_connection();
                        alldcr.Add(new TotalSaleDivisionWiseManagements
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