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
    public class GetExecutiveWiseNewYearBonazaController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getExecutiveWiseNewYearBonaza")]
        public HttpResponseMessage GetNewYearBonaza(GetExecutiveWiseNewYearBonaza ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    List<ExecutiveWiseNewYearBonaza> alldcr = new List<ExecutiveWiseNewYearBonaza>();
                    List<ExecutiveWiseNewYearBonazas> alldcr1 = new List<ExecutiveWiseNewYearBonazas>();
                    List<ExecutiveWiseNewYearBonazaFinal> YearBonaza = new List<ExecutiveWiseNewYearBonazaFinal>();
                    var dr = g1.return_dt("execwiseNewYearbonanzaSchemeWeb " + ula.ExId + "," + ula.index + "," + ula.Count + "," + ula.CIN);
                    bool more = false;
                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.Count + ula.index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }

                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new ExecutiveWiseNewYearBonazas
                            {
                                Party = dr.Rows[i]["Party"].ToString(),
                                mobile = dr.Rows[i]["mobile"].ToString(),
                                areanm = Convert.ToString(dr.Rows[i]["areanm"].ToString()),
                                cin = Convert.ToString(dr.Rows[i]["cin"].ToString()),
                                salesexname = dr.Rows[i]["salesexname"].ToString(),
                                WD = Convert.ToDecimal(dr.Rows[i]["WD"]),
                                WDPOINTS = Convert.ToDecimal(dr.Rows[i]["WDPOINTS"]),
                                LI = Convert.ToDecimal(dr.Rows[i]["LI"]),
                                LIPOINTS = Convert.ToDecimal(dr.Rows[i]["LIPOINTS"]),
                                mcb = Convert.ToDecimal(dr.Rows[i]["mcb"]),
                                mcbPOINTS = Convert.ToDecimal(dr.Rows[i]["mcbPOINTS"]),
                                WC = Convert.ToDecimal(dr.Rows[i]["WC"]),
                                WCPOINTS = Convert.ToDecimal(dr.Rows[i]["WCPOINTS"]),
                                TotalSale = Convert.ToDecimal(dr.Rows[i]["TotalSale"]),
                                TotalPOINTS = Convert.ToDecimal(dr.Rows[i]["TotalPOINTS"]),
                                CurrentSlab = dr.Rows[i]["CurrentSlab"].ToString(),
                                nextSlab = dr.Rows[i]["nextSlab"].ToString(),
                            });
                        }

                        YearBonaza.Add(new ExecutiveWiseNewYearBonazaFinal
                        {
                            NewSchemeYearBonaza = alldcr1,
                            ismore = more,
                        });

                        g1.close_connection();
                        alldcr.Add(new ExecutiveWiseNewYearBonaza
                        {
                            result = "True",
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = YearBonaza,
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}