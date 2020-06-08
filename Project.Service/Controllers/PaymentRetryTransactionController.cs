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
    public class PaymentRetryTransactionController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/RetryFreepayPaymentTransaction")]
        public HttpResponseMessage GetDetails(ListofPaymentRetryTransaction ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            try
            {
                string data1;
                string ordermsg = string.Empty;

                List<FreePayOutstandingPayments> alldcr = new List<FreePayOutstandingPayments>();
                List<FreePayOutstandingPayment> alldcr1 = new List<FreePayOutstandingPayment>();
                List<FreePayOutstandingInvoice> alldcr2 = new List<FreePayOutstandingInvoice>();
                string transid = cm.GenerateRandomString(19);

                var dr = g2.return_dt("sp_InvoiceFreePayretry '"                   
                    + ula.transactionid + "','"
                    +transid+"'"
                    );

                // ordermsg = "Order Placed and Order No:123456";

                g2.close_connection();

                if (dr.Rows.Count > 0)
                {
                    var dr1 = g2.return_dr("sp_customerreceiptaddInvoiceFreePayretry " + dr.Rows[0]["slno"].ToString());

                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            alldcr2.Add(new FreePayOutstandingInvoice
                            {
                                InvoiceId = dr1["adjustedid"].ToString(),
                                InvoiceAmount = dr1["invoiceamount"].ToString(),
                                CatId = dr1["catid"].ToString(),
                                DiscountedAmount = dr1["adjustedamount"].ToString(),
                                EnteredAmount = dr1["enteredamt"].ToString(),
                                SavedAmount = dr1["CDAMOUNT"].ToString(),
                                OutstandingAmount = dr1["prevbalamount"].ToString(),
                                Per = dr1["CDPER"].ToString(),
                            });
                        }
                    }

                    alldcr1.Add(new FreePayOutstandingPayment
                    {
                        transno = dr.Rows[0]["TRANSID"].ToString(),
                        Receipt = dr.Rows[0]["Receipt"].ToString(),
                        grandtotal = dr.Rows[0]["grandtotal"].ToString(),
                        savedamounttotal = dr.Rows[0]["savedtotal"].ToString(),
                        withdiscountamounttotal = dr.Rows[0]["distotal"].ToString(),
                        invoices = alldcr2
                    });

                    alldcr.Add(new FreePayOutstandingPayments
                    {
                        result = true,
                        message = "",
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
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Data not Inserted"), Encoding.UTF8, "application/json");

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
    }
}