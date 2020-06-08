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
    public class ChequeReturnExController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getChequeReturnExecutive")]
        public HttpResponseMessage GetAllUserLatLong(ListofchequeReturnEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    List<chequeReturnExs> alldcr = new List<chequeReturnExs>();
                    List<chequeReturnEx> alldcr1 = new List<chequeReturnEx>();
                    List<chequeReturnExFinal> chequeReturnExFinal = new List<chequeReturnExFinal>();
                    var dr = g1.return_dt("AppChequeReturnExcutive " + ula.ExId + ",'" + ula.CIN + "'," + Convert.ToBoolean(ula.Hierarchy) + "," + ula.index + "," + ula.Count);
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
                            alldcr1.Add(new chequeReturnEx
                            {
                                partynm = dr.Rows[i]["PartyName"].ToString(),
                                exnm = dr.Rows[i]["salesexname"].ToString(),
                                date = dr.Rows[i]["Voucherdt"].ToString(),
                                chequeno = dr.Rows[i]["chequeno"].ToString(),
                                amount = dr.Rows[i]["Cheque_amt"].ToString(),
                            });
                        }

                        chequeReturnExFinal.Add(new chequeReturnExFinal
                        {
                            chequeReturnEx = alldcr1,
                            ismore = more,
                        });

                        g1.close_connection();
                        alldcr.Add(new chequeReturnExs
                        {
                            result = "True",
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = chequeReturnExFinal,
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