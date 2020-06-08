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
    public class GetFreePayPaymentController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetFreePayPayment")]
        public HttpResponseMessage GetDetails(GetFreePayPaymentList ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != 0)
            {
                try
                {
                    string data1;

                    List<GetFreePayPayments> alldcr = new List<GetFreePayPayments>();
                    List<GetFreePayPayment> alldcr1 = new List<GetFreePayPayment>();

                    var dr = g2.return_dr("spGetFreePayPayment '" + ula.CIN + "','" + ula.fromdate + "','" + ula.todate + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetFreePayPayment
                            {
                                slno = Convert.ToInt32(dr["slno"].ToString()),
                                Receipt = Convert.ToString(dr["Receipt"].ToString()),
                                Voucherdt = Convert.ToString(dr["Voucherdt"].ToString()),
                                discoumtamt = Convert.ToDecimal(dr["discoumtamt"].ToString()),
                                status = Convert.ToString(dr["stat"].ToString()),
                                statusflag = Convert.ToInt32(dr["stat1"].ToString()),
                                totalamt = Convert.ToDecimal(dr["totalamt"].ToString()),
                                savedamt = Convert.ToDecimal(dr["savedamt"].ToString()),
                                transactionid = Convert.ToString(dr["TRANSID"].ToString()),
                                freepaytransactionid = Convert.ToString(dr["FREEPAYTRANSID"].ToString()),
                                retry = Convert.ToBoolean(dr["retry"].ToString()),
                            });
                        }
                        g2.close_connection();

                        alldcr.Add(new GetFreePayPayments
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}