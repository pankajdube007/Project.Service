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
    public class LastPaymentExController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getLastPaymentExecutive")]
        public HttpResponseMessage GetAllUserLatLong(ListofLastPaymentEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    List<LastPaymentExs> alldcr = new List<LastPaymentExs>();
                    List<LastPaymentEx> alldcr1 = new List<LastPaymentEx>();
                    List<LastPaymentExFinal> LastPaymentExFinal = new List<LastPaymentExFinal>();
                    var dr = g1.return_dt("AppLastPaymentExcutive " + ula.ExId + ",'" + ula.CIN + "'," + Convert.ToBoolean(ula.Hierarchy) + "," + ula.index + "," + ula.Count);
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
                            alldcr1.Add(new LastPaymentEx
                            {
                                slno = dr.Rows[i]["slno"].ToString(),
                                partynm = dr.Rows[i]["PartyName"].ToString(),
                                exnm = Convert.ToString(dr.Rows[i]["salesexname"].ToString()),
                                date = Convert.ToString(dr.Rows[i]["vdate"].ToString()),
                                instrumenttype = dr.Rows[i]["instrument"].ToString(),
                                chequeno = dr.Rows[i]["chequeno"].ToString(),
                                amount = dr.Rows[i]["amt"].ToString(),
                                status = dr.Rows[i]["stat"].ToString(),
                            });
                        }

                        LastPaymentExFinal.Add(new LastPaymentExFinal
                        {
                            LastPaymentEx = alldcr1,
                            ismore = more,
                        });

                        g1.close_connection();
                        alldcr.Add(new LastPaymentExs
                        {
                            result = "True",
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = LastPaymentExFinal,
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