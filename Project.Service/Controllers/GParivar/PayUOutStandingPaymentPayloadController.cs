using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.PayU;

namespace Project.Service.Controllers.PayU
{
    public class PayUOutStandingPaymentPayloadController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/PayUOutStandingPayload")]
        public HttpResponseMessage GetDetails(OutstandingPaymentByPayUModel ula)
        {
            var g2 = new DataConnectionTrans();
            var cm = new Common();
            var hashGeneration = new PayUHashGeneration();

            try
            {
                var minPayUMinAmt = Convert.ToDecimal(ConfigurationManager.AppSettings["PayUMinAmt"]);
                if (Convert.ToDecimal(ula.withdiscountamounttotal) < minPayUMinAmt)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content =
                        new StringContent(
                            cm.StatusTime(false,
                                $"Minimum payment should be greater than {minPayUMinAmt}"),
                            Encoding.UTF8, "application/json");

                    return response;
                }

                var openPayment = g2.return_dt("currentopentransaction '" + ula.CIN + "'");


                if (openPayment.Rows.Count > 0)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content =
                        new StringContent(
                            cm.StatusTime(false,
                                "One of your payment already in pending, Please try after some time !!!"),
                            Encoding.UTF8, "application/json");

                    return response;
                }


                var payUPayments = new List<PayUPayments>();
                var payUPayment = new List<PayUPayment>();
                var payUOutstandingInvoices = new List<PayUOutstandingInvoice>();
                var payUPayloads = new List<PayUPayload>();

                var dataTable = g2.return_dt("MakePayUPayload '"
                                             + ula.CIN + "','"
                                             + ula.grandtotal + "','"
                                             + ula.withdiscountamounttotal + "','"
                                             + ula.savedamounttotal + "','"
                                             + ula.OrderDetails + "','"
                                             + ula.devicetype + "','"
                                             + ula.deviceid + "'"
                );

                if (dataTable.Rows.Count > 0)
                {
                    var dr1 = g2.return_dr("sp_customerreceiptaddInvoiceFreePay " +
                                           dataTable.Rows[0]["CustomerReceiptId"]);

                    if (dr1.HasRows)
                        while (dr1.Read())
                            payUOutstandingInvoices.Add(new PayUOutstandingInvoice
                            {
                                InvoiceId = dr1["adjustedid"].ToString(),
                                InvoiceAmount = dr1["invoiceamount"].ToString(),
                                CatId = dr1["catid"].ToString(),
                                DiscountedAmount = dr1["adjustedamount"].ToString(),
                                EnteredAmount = dr1["enteredamt"].ToString(),
                                SavedAmount = dr1["CDAMOUNT"].ToString(),
                                OutstandingAmount = dr1["prevbalamount"].ToString(),
                                Per = dr1["CDPER"].ToString()
                            });
                    g2.close_connection();


                    //hash = sha512(key | txnid | amount | productinfo | firstname | email ||||||||||| SALT)
                    var hashVarsSeq =
                        ConfigurationManager.AppSettings["Gold.PayU.HashSequence"]
                            .Split('|'); // spliting hash sequence from config

                    var txnid = dataTable.Rows[0]["TransactionId"].ToString();
                    var amount = dataTable.Rows[0]["PaymentAmt"].ToString();
                    var productinfo = dataTable.Rows[0]["ProductInfo"].ToString();
                    var firstname = dataTable.Rows[0]["PartyFirstNameOrCIN"].ToString();
                    var email = dataTable.Rows[0]["PartyEmail"].ToString();
                    var phone = dataTable.Rows[0]["PartyPhone"].ToString();
                    var surl = ConfigurationManager.AppSettings["Gold.PayU.SUrl"];
                    var furl = ConfigurationManager.AppSettings["Gold.PayU.FUrl"];
                    var curl = ConfigurationManager.AppSettings["Gold.PayU.CUrl"];


                    var hashString = "";
                    foreach (var hashVar in hashVarsSeq)
                        if (hashVar == "txnid")
                            hashString = $"{hashString}{txnid}|";
                        else if (hashVar == "amount")
                            hashString = $"{hashString}{Convert.ToDecimal(amount):g29}|";
                        else if (hashVar == "productinfo")
                            hashString = $"{hashString}{productinfo}|";
                        else if (hashVar == "firstname")
                            hashString = $"{hashString}{firstname}|";
                        else if (hashVar == "email") hashString = $"{hashString}{email}";

                    var hash = hashGeneration.PaymentHash(hashString).ToLower(); //generating hash

                    payUPayloads.Add(new PayUPayload
                    {
                        txnid = txnid,
                        amount = amount,
                        productinfo = productinfo,
                        firstname = firstname,
                        email = email,
                        phone = phone.TrimStart('0'),
                        surl = surl,
                        furl = furl,
                        curl = curl,
                        hash = hash
                    });


                    payUPayment.Add(new PayUPayment
                    {
                        DiscTotal = dataTable.Rows[0]["DiscTotal"].ToString(), //100 cd
                        PaymentAmt = dataTable.Rows[0]["PaymentAmt"].ToString(), //900 payment
                        Invoices = payUOutstandingInvoices,
                        Payload = payUPayloads
                    });

                    payUPayments.Add(new PayUPayments
                    {
                        result = true,
                        message = "Please find the data from Payload array.",
                        servertime = DateTime.Now.ToString(),
                        data = payUPayment
                    });
                    var data1 = JsonConvert.SerializeObject(payUPayments,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    var response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content =
                        new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Difference in amount"),
                            Encoding.UTF8, "application/json");

                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content =
                    new StringContent(
                        cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message),
                        Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}

namespace Project.Service.Models.PayU
{
    public class PayUHashGeneration
    {
        private readonly string _key;
        private readonly string _salt;

        public PayUHashGeneration()
        {
            _key = ConfigurationManager.AppSettings["Gold.PayU.Key"];
            _salt = ConfigurationManager.AppSettings["Gold.PayU.Salt"];
        }

        public string PaymentHash(string text)
        {
            text = $"{_key}|{text}|||||||||||{_salt}";

            var message = Encoding.UTF8.GetBytes(text);

            var hashString = new SHA512Managed();
            var hashValue = hashString.ComputeHash(message);

            return hashValue.Aggregate("", (current, x) => current + $"{x:x2}");
        }

        public string VerifyHash()

        {
            return "";
        }
    }
}