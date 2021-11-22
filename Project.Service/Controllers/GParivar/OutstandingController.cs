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
    public class OutstandingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOutstanding")]
        public HttpResponseMessage GetDetails(ListsofOutstandingAction ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<Outstandings> alldcr = new List<Outstandings>();
                    List<Outstanding> alldcr1 = new List<Outstanding>();
                    var dr = g1.return_dt("App_Outstanding '" + ula.CIN + "'," + ula.Division);

                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new Outstanding
                            {
                                Due = Math.Round(Convert.ToDecimal(dr.Rows[i]["due"]) / 100000, 2).ToString("F2"),
                                OverDue = Math.Round(Convert.ToDecimal(dr.Rows[i]["overdue"].ToString()) / 100000, 2).ToString("F2"),
                                Outstandings = Math.Round(Convert.ToDecimal(dr.Rows[i]["outstanding"].ToString()) / 100000, 2).ToString("F2"),
                                onlinepayment = Convert.ToBoolean(dr.Rows[i]["isonlinepaymnet"]),
                                channel = Convert.ToBoolean(dr.Rows[0]["channel"]),
                                freepay = Convert.ToBoolean(dr.Rows[0]["freepay"]),
                                payu = Convert.ToBoolean(dr.Rows[0]["payu"]),
                                fanfilter = Convert.ToBoolean(dr.Rows[0]["fanfilter"]),
                                isregistered = Convert.ToBoolean(dr.Rows[i]["freepayregister"]),
                                IsActive = Convert.ToBoolean(dr.Rows[i]["IsActive"]),
                                errormsg = Convert.ToString(dr.Rows[i]["errormsg"]),
                                duesquence = Convert.ToBoolean(dr.Rows[i]["duesquence"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new Outstandings
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