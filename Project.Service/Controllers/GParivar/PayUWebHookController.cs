using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.PayU;

namespace Project.Service.Controllers.PayU
{
    public class PayUWebHookController : ApiController
    {
        private const string Command = "verify_payment";
        private const string StatusCommand = "get_transaction_info";
        private const string SattlementCommand = "get_settlement_details";

        private readonly Common _cm;
        private readonly string _payUMoneyKey;
        private readonly string _payUMoneySalt;
        private readonly string _payUMoneyUrl;
        private readonly string _payUMoneyVerifyUrl;
        private readonly DataConnectionTrans _trans;

        public PayUWebHookController()
        {
            _payUMoneyUrl = ConfigurationManager.AppSettings["Gold.PayU.Url"];
            _payUMoneyKey = ConfigurationManager.AppSettings["Gold.PayU.Key"];
            _payUMoneySalt = ConfigurationManager.AppSettings["Gold.PayU.Salt"];
            _payUMoneyVerifyUrl = ConfigurationManager.AppSettings["Gold.PayU.VerifyUrl"];
            _trans = new DataConnectionTrans();
            _cm = new Common();
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/gold-web-hook/success")]
        public HttpResponseMessage ReceiveSuccessResponse(PayUResponseIm resp)
        {
            HttpResponseMessage response;
            var im = JsonConvert.DeserializeObject<PayUResponseDto>(resp.PayuResponse);
            if (string.Equals(_payUMoneyKey, im.key))
            {
                _trans.ExecDB($"PayUTransactionStatusAdd '{im.id}','{im.mode}','{im.status}','{im.unmappedstatus}'," +
                              $"'{im.key}','{im.txnid}','{im.transaction_fee}','{im.amount}'," +
                              $"'{im.discount}','{im.addedon}','{im.productinfo}','{im.firstname}'," +
                              $"'{im.email}','{im.phone}','{im.field1}','{im.field2}'," +
                              $"'{im.field3}','{im.field4}','{im.field5}','{im.field6}'," +
                              $"'{im.field7}','{im.field8}','{im.field9}','{im.payment_source}'," +
                              $"'{im.PG_TYPE}','{im.bank_ref_no}','{im.ibibo_code}','{im.error_code}'," +
                              $"'{im.Error_Message}','{im.is_seamless}',1,'Success callback API from App'");

                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content =
                    new StringContent(_cm.StatusTime(true, "Status Added Successfully."), Encoding.UTF8,
                        "application/json");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content =
                    new StringContent(_cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");
            }


            return response;
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/payu-payment-verify/txnid")]
        public HttpResponseMessage PayUPaymentVerification(PayUPaymentVerifyIm verifyIm)
        {
            HttpResponseMessage response;
            var payUPaymentVerifyList = new List<PayUPaymentVerify>();
            var transactionDetails = new List<TransactionDetails>();
            if (verifyIm.ClientSecret != "")
            {
                var hashstr = _payUMoneyKey + "|" + Command + "|" + verifyIm.txnid + "|" + _payUMoneySalt;


                var hash = GenerateHash512(hashstr);

                ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol =
                    (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                var request = (HttpWebRequest)WebRequest.Create(_payUMoneyVerifyUrl);

                var postData = "key=" + Uri.EscapeDataString(_payUMoneyKey);
                postData += "&hash=" + Uri.EscapeDataString(hash);
                postData += "&var1=" + Uri.EscapeDataString(verifyIm.txnid);
                postData += "&command=" + Uri.EscapeDataString(Command);
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response1 = (HttpWebResponse)request.GetResponse();

                var responseString =
                    new StreamReader(response1.GetResponseStream() ?? throw new InvalidOperationException())
                        .ReadToEnd();

                var details = JObject.Parse(responseString);

                var status = details["status"].ToString();
                //var transactionDetails = details["transaction_details"]["'" + verifyIm.txnid + "'"].ToString();
                var paymentTransId = details["transaction_details"].SelectToken(verifyIm.txnid).ToString();
                var transactionDetail = JsonConvert.DeserializeObject<TransactionDetails>(paymentTransId);
                transactionDetails.Add(transactionDetail);

                //{ "status":0,"msg":"0 out of 1 Transactions Fetched Successfully","transaction_details":{ "Kol214000359145234":{ "mihpayid":"Not Found","status":"Not Found"} } }
                //{
                //    "mihpayid": "12087218271",
                //    "request_id": "",
                //    "bank_ref_num": "IGAKHGXUP5",
                //    "amt": "2.00",
                //    "transaction_amount": "2.00",
                //    "txnid": "Kol21400033029118887",
                //    "additional_charges": "0.00",
                //    "productinfo": "1a6e4b34b4b4d1b5",
                //    "firstname": "2140003",
                //    "bankcode": "SBIB",
                //    "udf1": "udf1",
                //    "udf3": "udf3",
                //    "udf4": "udf4",
                //    "udf5": "udf5",
                //    "field2": null,
                //    "field9": "Paid",
                //    "error_code": "E000",
                //    "addedon": "2021-01-18 11:31:42",
                //    "payment_source": "payu",
                //    "card_type": null,
                //    "error_Message": "NO ERROR",
                //    "net_amount_debit": 2,
                //    "disc": "0.00",
                //    "mode": "NB",
                //    "PG_TYPE": "NB-PG",
                //    "card_no": "",
                //    "udf2": "udf2",
                //    "status": "success",
                //    "unmappedstatus": "captured",
                //    "Merchant_UTR": null,
                //    "Settled_At": "0000-00-00 00:00:00"
                //}

                if (status == "0")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.Content =
                        new StringContent(
                            _cm.StatusTime(false, $"No data found at PayU with this {verifyIm.txnid} transactionId"),
                            Encoding.UTF8, "application/json");
                }
                else
                {
                    _trans.ExecDB(
                        $"PayuPaymentUpdate '{verifyIm.txnid}','{transactionDetail.mihpayid}','{transactionDetail.status}','{GetClientIp()}','{paymentTransId}'");

                    //Update force status check api
                    //_trans.ExecDB($"PayUTransactionStatusAdd '{transactionDetail.mihpayid}','{transactionDetail.mode}','{transactionDetail.status}','{transactionDetail.unmappedstatus}'," +
                    //              $"'{transactionDetail.key}','{transactionDetail.txnid}','{transactionDetail.transaction_amount}','{transactionDetail.amt}'," +
                    //              $"'{transactionDetail.disc}','{transactionDetail.addedon}','{transactionDetail.productinfo}','{transactionDetail.firstname}'," +
                    //              $"'{transactionDetail.email}','{transactionDetail.phone}','{transactionDetail.field1}','{transactionDetail.field2}'," +
                    //              $"'{transactionDetail.field3}','{transactionDetail.field4}','{transactionDetail.field5}','{transactionDetail.field6}'," +
                    //              $"'{transactionDetail.field7}','{transactionDetail.field8}','{transactionDetail.field9}','{transactionDetail.payment_source}'," +
                    //              $"'{transactionDetail.PG_TYPE}','{transactionDetail.bank_ref_num}','{transactionDetail.ibibo_code}','{transactionDetail.error_code}'," +
                    //              $"'{transactionDetail.error_Message}','{transactionDetail.is_seamless}',1,'Status check API'");


                    payUPaymentVerifyList.Add(new PayUPaymentVerify
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = transactionDetails
                    });
                    var data1 = JsonConvert.SerializeObject(payUPaymentVerifyList,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


                    response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content =
                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content =
                    new StringContent(_cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");
            }

            return response;
        }

        [HttpPost]
        [ValidateModel]

        [Route("api/payu-payment-verify/from-and-to-date")]
        public HttpResponseMessage PayUPaymentVerificationBetweenTwoDates(PayUPaymentVerificationByDatesIm verifyIm)
        {
            HttpResponseMessage response;
            var payUPaymentVerifyList = new List<PayUPaymentVerify>();
            var transactionDetails = new List<TransactionDetails>();
            if (verifyIm.ClientSecret != "")
            {
                var fromDate = verifyIm.FromDate == null
                    ? DateTime.Now.AddMinutes(-20).ToString("yyyy-MM-dd HH:mm:ss")
                    : verifyIm.FromDate?.ToString("yyyy-MM-dd HH:mm:ss");

                var toDate = verifyIm.ToDate == null
                    ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    : verifyIm.ToDate?.ToString("yyyy-MM-dd HH:mm:ss");
                var hashstr = _payUMoneyKey + "|" + StatusCommand + "|" + fromDate + "|" + _payUMoneySalt;


                var hash = GenerateHash512(hashstr);

                ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol =
                    (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                var request = (HttpWebRequest)WebRequest.Create(_payUMoneyVerifyUrl);

                //var postData = "key=" + Uri.EscapeDataString(_payUMoneyKey);
                //postData += "&hash=" + Uri.EscapeDataString(hash);
                //postData += "&var1=" + Uri.EscapeDataString(fromDate);
                //postData += "&var2=" + Uri.EscapeDataString(toDate);
                //postData += "&command=" + Uri.EscapeDataString(StatusCommand);
                var postData = "key=" + _payUMoneyKey;
                postData += "&hash=" + hash;
                postData += "&var1=" + fromDate;
                postData += "&var2=" + toDate;
                postData += "&command=" + StatusCommand;
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response1 = (HttpWebResponse)request.GetResponse();

                var responseString =
                    new StreamReader(response1.GetResponseStream() ?? throw new InvalidOperationException())
                        .ReadToEnd();

                var payUPaymentVerificationByDates =
                    JsonConvert.DeserializeObject<PayUPaymentVerificationByDates>(responseString);


                if (payUPaymentVerificationByDates.status == "0")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.Content =
                        new StringContent(
                            _cm.StatusTime(false,
                                $"No data found at PayU with this {verifyIm.FromDate} and {verifyIm.ToDate} date range with message {payUPaymentVerificationByDates.msg}"),
                            Encoding.UTF8, "application/json");
                }
                else
                {
                    foreach (var payTransactionDetail in payUPaymentVerificationByDates.Transaction_details)
                        _trans.ExecDB(
                            $"PayuPaymentUpdate '{payTransactionDetail.txnid}','{payTransactionDetail.id}','{payTransactionDetail.status}','{GetClientIp()}','{JsonConvert.SerializeObject(payTransactionDetail)}'");


                    //Update force status check api
                    //_trans.ExecDB($"PayUTransactionStatusAdd '{transactionDetail.mihpayid}','{transactionDetail.mode}','{transactionDetail.status}','{transactionDetail.unmappedstatus}'," +
                    //              $"'{transactionDetail.key}','{transactionDetail.txnid}','{transactionDetail.transaction_amount}','{transactionDetail.amt}'," +
                    //              $"'{transactionDetail.disc}','{transactionDetail.addedon}','{transactionDetail.productinfo}','{transactionDetail.firstname}'," +
                    //              $"'{transactionDetail.email}','{transactionDetail.phone}','{transactionDetail.field1}','{transactionDetail.field2}'," +
                    //              $"'{transactionDetail.field3}','{transactionDetail.field4}','{transactionDetail.field5}','{transactionDetail.field6}'," +
                    //              $"'{transactionDetail.field7}','{transactionDetail.field8}','{transactionDetail.field9}','{transactionDetail.payment_source}'," +
                    //              $"'{transactionDetail.PG_TYPE}','{transactionDetail.bank_ref_num}','{transactionDetail.ibibo_code}','{transactionDetail.error_code}'," +
                    //              $"'{transactionDetail.error_Message}','{transactionDetail.is_seamless}',1,'Status check API'");


                    response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content =
                        response.Content =
                            new StringContent(
                                _cm.StatusTime(false,
                                    $"Total {payUPaymentVerificationByDates.Transaction_details.Count} transaction fetch"),
                                Encoding.UTF8, "application/json");
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content =
                    new StringContent(_cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");
            }

            return response;
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/payu-payment/sattlement")]
        public HttpResponseMessage SattlementDetails(SattlementDetailsIm verifyIm)
        {
            HttpResponseMessage response;

            if (verifyIm.ClientSecret != "")
            {
                var fromDate = verifyIm.Date == null
                    ? DateTime.Now.ToString("yyyy-MM-dd")
                    : verifyIm.Date?.ToString("yyyy-MM-dd");


                var hashstr = _payUMoneyKey + "|" + SattlementCommand + "|" + fromDate + "|" + _payUMoneySalt;
                var hash = GenerateHash512(hashstr);

                ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol =
                    (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                var request = (HttpWebRequest)WebRequest.Create(_payUMoneyVerifyUrl);

                var postData = "key=" + _payUMoneyKey;
                postData += "&hash=" + hash;
                postData += "&var1=" + fromDate;
                postData += "&command=" + SattlementCommand;
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response1 = (HttpWebResponse)request.GetResponse();

                var responseString =
                    new StreamReader(response1.GetResponseStream() ?? throw new InvalidOperationException())
                        .ReadToEnd();

                var payUPaymentVerificationByDates =
                    JsonConvert.DeserializeObject<SattlementDetailsVm>(responseString);


                if (payUPaymentVerificationByDates.status == "0")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.Content =
                        new StringContent(
                            _cm.StatusTime(false,
                                $"No data found at PayU with this {verifyIm.Date} date with message {payUPaymentVerificationByDates.msg}"),
                            Encoding.UTF8, "application/json");
                }
                else
                {
                    foreach (var payTransactionDetail in payUPaymentVerificationByDates.Txn_details)
                    {
                        _trans.ExecDB($"PayUSettlementStatusUpdate '{fromDate}','{payTransactionDetail.payuid}','{payTransactionDetail.txnid}','{payTransactionDetail.txndate}','{payTransactionDetail.mode}'," +
                                      $"'{payTransactionDetail.amount}','{payTransactionDetail.requestid}','{payTransactionDetail.requestdate}','{payTransactionDetail.requestaction}'," +
                                      $"'{payTransactionDetail.requestamount}','{payTransactionDetail.mer_utr}','{payTransactionDetail.mer_service_fee}','{payTransactionDetail.mer_service_tax}'," +
                                      $"'{payTransactionDetail.mer_net_amount}','{payTransactionDetail.bank_name}','{payTransactionDetail.issuing_bank}','{payTransactionDetail.merchant_subvention_amount}'," +
                                      $"'{payTransactionDetail.cgst}','{payTransactionDetail.igst}','{payTransactionDetail.sgst}','{payTransactionDetail.PG_TYPE}'," +
                                      $"'{payTransactionDetail.CardType}'");
                    }

                    response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content =
                        response.Content =
                            new StringContent(
                                _cm.StatusTime(true,
                                    $"Total {payUPaymentVerificationByDates.Txn_details.Count} transaction fetch"),
                                Encoding.UTF8, "application/json");
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content =
                    new StringContent(_cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");
            }

            return response;
        }

        private static string GenerateHash512(string text)
        {
            var message = Encoding.UTF8.GetBytes(text);


            var hashString = new SHA512Managed();
            var hashValue = hashString.ComputeHash(message);

            return hashValue.Aggregate("", (current, x) => current + $"{x:x2}");
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;

            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                var prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }

            if (HttpContext.Current != null)
                return HttpContext.Current.Request.UserHostAddress;
            return null;
        }
    }
}