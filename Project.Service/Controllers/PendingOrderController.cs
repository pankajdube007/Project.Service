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
    public class PendingOrderController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getPendingOrders")]
        public HttpResponseMessage GetDetails(ListsofPendingOrderAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<PendingOrders> alldcr = new List<PendingOrders>();
                    List<PendingOrder> alldcr1 = new List<PendingOrder>();
                    List<PendingOrder1> alldcr2 = new List<PendingOrder1>();

                    var dr = g1.return_dt("App_PendingOrder '" + ula.CIN + "'," + ula.Division + ",'" + ula.SearchText + "'," + ula.Index + "," + ula.Count + ",'" + ula.AsonDate + "'," + ula.Type);

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
                            alldcr2.Add(new PendingOrder1
                            {
                                ItemName = Convert.ToString(dr.Rows[i]["itemnm"].ToString()),
                                ItemCode = Convert.ToString(dr.Rows[i]["itemcode"].ToString()),
                                Colornm = Convert.ToString(dr.Rows[i]["colornm"].ToString()),
                                PoDt = Convert.ToString(dr.Rows[i]["podt"].ToString()),
                                PoNum = Convert.ToString(dr.Rows[i]["ponum"].ToString().Replace("SOTR/", string.Empty)),
                                Amount = Convert.ToString(dr.Rows[i]["withtax"].ToString()),
                                PendingQty = Convert.ToString(dr.Rows[i]["pending"].ToString()),
                            });
                        }

                        alldcr1.Add(new PendingOrder
                        {
                            pendingdata = alldcr2,
                            ismore = more,
                        });

                        g1.close_connection();
                        alldcr.Add(new PendingOrders
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