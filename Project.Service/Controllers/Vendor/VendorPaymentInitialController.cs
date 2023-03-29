using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class VendorPaymentInitialController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/CheckVendorPaymentInitial")]
        public HttpResponseMessage GetDetails(ListofVendorPaymentInitial ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            try
            {

                var drcheck = g2.return_dr("currentVendoropentransaction '" + ula.CIN + "'");


                if (drcheck.HasRows)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "One of your payment already in pending, Please try after some time !!!"), Encoding.UTF8, "application/json");

                    return response;
                }
                else
                {

                    string data1;
                    string ordermsg = string.Empty;

                    List<VendorPaymentInitials> alldcr = new List<VendorPaymentInitials>();
                    List<VendorPaymentInitial> alldcr1 = new List<VendorPaymentInitial>();
                    List<VendorPaymentInitialInvoice> alldcr2 = new List<VendorPaymentInitialInvoice>();
                    //string transid = cm.GenerateRandomString(19);

                    var dr = g2.return_dt("vendorpaymentintiated '"
                        + ula.CIN + "','"
                        + ula.grandtotal + "','"
                        + ula.paytotal + "','"
                        + ula.interestamounttotal + "','"
                        + ula.OrderDetails + "','"
                        + ula.devicetype + "','"
                        + ula.deviceid + "'"
                        );

                    // ordermsg = "Order Placed and Order No:123456";

                    g2.close_connection();

                    if (dr.Rows.Count > 0)
                    {
                        var dr1 = g2.return_dr("vendorpaymentintiatedchild " + dr.Rows[0]["slno"].ToString());

                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                alldcr2.Add(new VendorPaymentInitialInvoice
                                {
                                    InvoiceId = dr1["adjustedid"].ToString(),
                                    CatId = dr1["catid"].ToString(),
                                    InvoiceAmount = dr1["invoiceamount"].ToString(),
                                    PayAmount = dr1["adjustedamount"].ToString(),
                                    EnteredAmount = dr1["enteredamt"].ToString(),
                                    InterestAmount = dr1["INTAMOUNT"].ToString(),
                                    OutstandingAmount = dr1["prevbalamount"].ToString(),
                                    Per = dr1["INTPER"].ToString(),
                                });
                            }
                        }

                        alldcr1.Add(new VendorPaymentInitial
                        {
                            transno = dr.Rows[0]["TRANSID"].ToString(),
                            Receipt = dr.Rows[0]["Receipt"].ToString(),
                            grandtotal = dr.Rows[0]["grandtotal"].ToString(),
                            interestamounttotal = dr.Rows[0]["interestamt"].ToString(),
                            paytotal = dr.Rows[0]["paytotal"].ToString(),
                            invoices = alldcr2
                        });

                        alldcr.Add(new VendorPaymentInitials
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
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Difference in amount"), Encoding.UTF8, "application/json");

                        return response;
                    }
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