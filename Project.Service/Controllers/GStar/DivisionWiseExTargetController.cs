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
    public class DivisionWiseExTargetController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDivisionWiseExTarget")]
        public HttpResponseMessage GetDetails(ListsofDivisionWiseExTarget ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<DivisionWiseExTargets> alldcr = new List<DivisionWiseExTargets>();
                    List<DivisionWiseExTarget> alldcr1 = new List<DivisionWiseExTarget>();

                    var dr = g1.return_dt("AppDivisioWiseExTarget " + ula.ExId);

                    if (!dr.Columns.Contains("WIRING DEVICES"))
                    {
                        dr.Columns.Add("WIRING DEVICES", typeof(string));
                        dr.Columns["WIRING DEVICES"].Expression = "'0'";
                    }
                    if (!dr.Columns.Contains("LIGHTS"))
                    {
                        dr.Columns.Add("LIGHTS", typeof(string));
                        dr.Columns["LIGHTS"].Expression = "'0'";
                    }
                    if (!dr.Columns.Contains("WIRE & CABLE"))
                    {
                        dr.Columns.Add("WIRE & CABLE", typeof(string));
                        dr.Columns["WIRE & CABLE"].Expression = "'0'";
                    }
                    if (!dr.Columns.Contains("PIPES & FITTINGS"))
                    {
                        dr.Columns.Add("PIPES & FITTINGS", typeof(string));
                        dr.Columns["PIPES & FITTINGS"].Expression = "'0'";
                    }

                    if (!dr.Columns.Contains("MCB & DBS"))
                    {
                        dr.Columns.Add("MCB & DBS", typeof(string));
                        dr.Columns["MCB & DBS"].Expression = "'0'";
                    }

                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new DivisionWiseExTarget
                            {
                                ExId = Convert.ToInt32(dr.Rows[i]["SlNo"]),
                                salesexnm = Convert.ToString(dr.Rows[i]["salesexnm"]),
                                wiredevice = Math.Round(Convert.ToDecimal(dr.Rows[i]["WIRING DEVICES"]), 0).ToString(),
                                lights = Math.Round(Convert.ToDecimal(dr.Rows[i]["LIGHTS"]), 0).ToString(),
                                wireandcable = Math.Round(Convert.ToDecimal(dr.Rows[i]["WIRE & CABLE"]), 0).ToString(),
                                pipingandfitting = Math.Round(Convert.ToDecimal(dr.Rows[i]["PIPES & FITTINGS"]), 0).ToString(),
                                mcbanddcb = Math.Round(Convert.ToDecimal(dr.Rows[i]["MCB & DBS"]), 0).ToString(),
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new DivisionWiseExTargets
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