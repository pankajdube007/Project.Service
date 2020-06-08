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
    public class CategoryWiseSale_ExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getCategoryWiseSaleExcutive")]
        public HttpResponseMessage GetDetails(ListsofCategoryWiseEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<CategoryWiseExs> alldcr = new List<CategoryWiseExs>();
                    List<CategoryWiseEx> alldcr1 = new List<CategoryWiseEx>();
                    List<CategoryWiseFinalEx> Final = new List<CategoryWiseFinalEx>();
                    List<CategoryWiseTotalEx> Total = new List<CategoryWiseTotalEx>();

                    var dr = g1.return_dt("AppCategorywisesaleExcutive " + ula.ExId + ",'" + ula.CIN + "','" + ula.SubExId + "','" + ula.DistrictId + "','" + ula.AreaId + "','" + ula.BranchId + "'," + Convert.ToBoolean(ula.Hierarchy) + ",'" + ula.FromDate + "','" + ula.ToDate + "'");

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

                    decimal wiredevicetotal = 0;
                    decimal lightstotal = 0;
                    decimal wireandcabletotal = 0;
                    decimal pipingandfittingtotal = 0;
                    decimal mcbanddcbtotal = 0;

                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new CategoryWiseEx
                            {
                                partynm = Convert.ToString(dr.Rows[i]["displaynm"]),
                                wiredevice = Math.Round(Convert.ToDecimal(dr.Rows[i]["WIRING DEVICES"]), 0).ToString(),
                                lights = Math.Round(Convert.ToDecimal(dr.Rows[i]["LIGHTS"]), 0).ToString(),
                                wireandcable = Math.Round(Convert.ToDecimal(dr.Rows[i]["WIRE & CABLE"]), 0).ToString(),
                                pipingandfitting = Math.Round(Convert.ToDecimal(dr.Rows[i]["PIPES & FITTINGS"]), 0).ToString(),
                                mcbanddcb = Math.Round(Convert.ToDecimal(dr.Rows[i]["MCB & DBS"]), 0).ToString(),
                            });

                            wiredevicetotal = wiredevicetotal + Convert.ToDecimal(dr.Rows[i]["WIRING DEVICES"]);
                            lightstotal = lightstotal + Convert.ToDecimal(dr.Rows[i]["LIGHTS"]);
                            wireandcabletotal = wireandcabletotal + Convert.ToDecimal(dr.Rows[i]["WIRE & CABLE"]);
                            pipingandfittingtotal = pipingandfittingtotal + Convert.ToDecimal(dr.Rows[i]["PIPES & FITTINGS"]);
                            mcbanddcbtotal = mcbanddcbtotal + Convert.ToDecimal(dr.Rows[i]["MCB & DBS"]);
                        }

                        Total.Add(new CategoryWiseTotalEx
                        {
                            wiredevicetotal = Math.Round(wiredevicetotal, 0).ToString(),
                            lightstotal = Math.Round(lightstotal, 0).ToString(),
                            wireandcabletotal = Math.Round(wireandcabletotal, 0).ToString(),
                            pipingandfittingtotal = Math.Round(pipingandfittingtotal, 0).ToString(),
                            mcbanddcbtotal = Math.Round(mcbanddcbtotal, 0).ToString(),
                            finaltotal = (Math.Round(wiredevicetotal+ lightstotal+ wireandcabletotal + pipingandfittingtotal + mcbanddcbtotal, 0)).ToString()
                        });

                        Final.Add(new CategoryWiseFinalEx
                        {
                            CategoryWisedata = alldcr1,
                            Totaldata = Total
                        });

                        g1.close_connection();
                        alldcr.Add(new CategoryWiseExs
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = Final,
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