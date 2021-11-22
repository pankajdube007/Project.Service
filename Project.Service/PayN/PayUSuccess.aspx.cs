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
using System.Web.UI;
using Newtonsoft.Json;
using Project.Service.Models;

namespace Project.Service.PayN
{
    /// <summary>
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class PayUSuccess : Page
    {
        private string PaymentStatus = string.Empty;
        private string PaymentTransactionId = string.Empty;
        private string TransactionId = string.Empty;

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            /* Response received from Payment Gateway at this page.
            Process response parameters to generate Hash signature and compare with Hash sent by payment gateway 
            to verify response content. Response may contain additional charges parameter so depending on that 
            two order of strings are used in this kit.

            Hash string without Additional Charges -
            hash = sha512(SALT|status||||||udf5|||||email|firstname|productinfo|amount|txnid|key)

            With additional charges - 
            hash = sha512(additionalCharges|SALT|status||||||udf5|||||email|firstname|productinfo|amount|txnid|key)

            */
            var trans = new DataConnectionTrans();
            var responseStr = "{";
            foreach (string strKey in Request.Form)
            {

                //responseStr += "'" + strKey + "'" + ":" + "'" + Request.Form[strKey] + "',";
                //responseStr += "\"" + strKey + "\"" + ":" + "\"" + Request.Form[strKey] + "\",";
                responseStr += $"\"{ strKey }\":\"{Request.Form[strKey]}\",";
            }

            responseStr = responseStr.TrimEnd(',');
            responseStr += "}";
            try
            {
                string[] merc_hash_vars_seq;
                var merc_hash_string = string.Empty;
                var merc_hash = string.Empty;
                var order_id = string.Empty;

                PaymentStatus = Request.Form["status"];
                PaymentTransactionId = Request.Form["mihpayid"];
                TransactionId = Request.Form["txnid"];


                //string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
                var hash_seq = ConfigurationManager.AppSettings["Gold.PayU.HashSequence"];

                if (Request.Form["status"] == "success")
                {
                    merc_hash_vars_seq = hash_seq.Split('|');
                    Array.Reverse(merc_hash_vars_seq);
                    merc_hash_string = ConfigurationManager.AppSettings["Gold.PayU.Salt"] + "|" +
                                       Request.Form["status"];
                    //Check for presence of additionalCharges and include in hash
                    if (Request.Form["additionalCharges"] != null)
                        merc_hash_string = Request.Form["additionalCharges"] + "|" +
                                           ConfigurationManager.AppSettings["Gold.PayU.Salt"] + "|" +
                                           Request.Form["status"];

                    foreach (var merc_hash_var in merc_hash_vars_seq)
                    {
                        merc_hash_string += "|";
                        merc_hash_string += Request.Form[merc_hash_var] ?? "";
                    }

                    //Calculate response hash to verify	
                    merc_hash = GenerateHash512(merc_hash_string).ToLower();


                    //Comapre status and hash. Hash verification is mandatory.
                    if (merc_hash != Request.Form["hash"])

                    {
                        trans.ExecDB(
                            $"PayuPaymentUpdate '{TransactionId}','{PaymentTransactionId}','{PaymentStatus}:HashNotMatch','{GetClientIp()}','{responseStr}'");
                        Response.Write("<h2>Hash value did not match</h2>");
                    }
                    else
                    {
                        order_id = Request.Form["txnid"];
                        Response.Write("<h2>Payment Response-</h2><br />");


                        //Response.Write("<h2>Hash Verified...</h2><br />");

                        if (VerifyPayment(order_id, Request.Form["mihpayid"]))
                        {
                            trans.ExecDB(
                                $"PayuPaymentUpdate '{TransactionId}','{PaymentTransactionId}','{PaymentStatus}','{GetClientIp()}','{responseStr}'");
                            //TODO:Add response to payload table
                            Response.Write("<h2>Your Payment has been successfully Verified...</h2><br />");
                        }
                        else
                        {
                            trans.ExecDB(
                                $"PayuPaymentUpdate '{TransactionId}','{PaymentTransactionId}','{PaymentStatus}:NotVerified','{GetClientIp()}','{responseStr}'");
                            Response.Write("<h2>Payment Verification Failed...</h2><br />");
                            //Hash value did not matched
                        }
                    }
                }

                else
                {
                    trans.ExecDB(
                        $"PayuPaymentUpdate '{TransactionId}','{PaymentTransactionId}','{PaymentStatus}','{GetClientIp()}','{responseStr}'");
                    Response.Write("<h2>Payment failed or cancelled</h2>");
                    // osc_redirect(osc_href_link(FILENAME_CHECKOUT, 'payment' , 'SSL', null, null,true));
                }
            }

            catch (Exception ex)
            {
                trans.ExecDB(
                    $"PayuPaymentUpdate '{TransactionId}','{PaymentTransactionId}','{PaymentStatus}:{ex.Message}','{GetClientIp()}','{responseStr}'");
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");
            }
        }


        //This function is used to double check payment
        private bool VerifyPayment(string txnid, string mihpayid)
        {
            var command = "verify_payment";
            var hashstr = ConfigurationManager.AppSettings["Gold.PayU.Key"] + "|" + command + "|" + txnid + "|" +
                          ConfigurationManager.AppSettings["Gold.PayU.Salt"];

            var hash = GenerateHash512(hashstr);

            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol =
                (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            var request =
                (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Gold.PayU.VerifyUrl"]);

            var postData = "key=" + Uri.EscapeDataString(ConfigurationManager.AppSettings["Gold.PayU.Key"]);
            postData += "&hash=" + Uri.EscapeDataString(hash);
            postData += "&var1=" + Uri.EscapeDataString(txnid);
            postData += "&command=" + Uri.EscapeDataString(command);
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            if (responseString.Contains("\"mihpayid\":\"" + mihpayid + "\"") &&
                responseString.Contains("\"status\":\"success\""))
                return true;
            return false;
            /*
            Here is json response example -

            {"status":1,
            "msg":"1 out of 1 Transactions Fetched Successfully",
            "transaction_details":</strong>
            {	
                "Txn72738624":
                {
                    "mihpayid":"403993715519726325",
                    "request_id":"",
                    "bank_ref_num":"670272",
                    "amt":"6.17",
                    "transaction_amount":"6.00",
                    "txnid":"Txn72738624",
                    "additional_charges":"0.17",
                    "productinfo":"P01 P02",
                    "firstname":"Viatechs",
                    "bankcode":"CC",
                    "udf1":null,
                    "udf3":null,
                    "udf4":null,
                    "udf5":"PayUBiz_PHP7_Kit",
                    "field2":"179782",
                    "field9":" Verification of Secure Hash Failed: E700 -- Approved -- Transaction Successful -- Unable to be determined--E000",
                    "error_code":"E000",
                    "addedon":"2019-08-09 14:07:25",
                    "payment_source":"payu",
                    "card_type":"MAST",
                    "error_Message":"NO ERROR",
                    "net_amount_debit":6.17,
                    "disc":"0.00",
                    "mode":"CC",
                    "PG_TYPE":"AXISPG",
                    "card_no":"512345XXXXXX2346",
                    "name_on_card":"Test Owenr",
                    "udf2":null,
                    "status":"success",
                    "unmappedstatus":"captured",
                    "Merchant_UTR":null,
                    "Settled_At":"0000-00-00 00:00:00"
                }
            }
            }

            Decode the Json response and retrieve "transaction_details" 
            Then retrieve {txnid} part. This is dynamic as per txnid sent in var1.
            Then check for mihpayid and status.

            */
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
            if (!IsRequestAvailable(HttpContext.Current))
                return string.Empty;

            var result = string.Empty;
            if (HttpContext.Current.Request.Headers != null)
            {
                //in some cases server use other HTTP header
                //in these cases an administrator can specify a custom Forwarded HTTP header
                //e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc
                var forwardedHttpHeader = "NULL"; //GetConfig<string>(GoldConstants.AppSetting.ForwardedHttpHeader);
                if (forwardedHttpHeader.Equals("NULL", StringComparison.InvariantCultureIgnoreCase))
                    //The X-Forwarded-For (XFF) HTTP header field is a de facto standard
                    //for identifying the originating IP address of a client
                    //connecting to a web server through an HTTP proxy or load balancer.
                    forwardedHttpHeader = "X-FORWARDED-FOR";
                //it's used for identifying the originating IP address of a client connecting to a web server
                //through an HTTP proxy or load balancer.
                var xff = HttpContext.Current.Request.Headers.AllKeys
                    .Where(x => forwardedHttpHeader.Equals(x, StringComparison.InvariantCultureIgnoreCase))
                    .Select(k => HttpContext.Current.Request.Headers[k])
                    .FirstOrDefault();

                //if you want to exclude private IP addresses, then see http://stackoverflow.com/questions/2577496/how-can-i-get-the-clients-ip-address-in-asp-net-mvc
                if (string.IsNullOrWhiteSpace(xff))
                {
                    var lastIp = xff?.Split(',').FirstOrDefault();
                    result = lastIp;
                }
            }

            if (string.IsNullOrWhiteSpace(result) && HttpContext.Current.Request.UserHostAddress != null)
                result = HttpContext.Current.Request.UserHostAddress;

            //some validation
            if (result == "::1")
                result = "127.0.0.1";
            //remove port
            if (string.IsNullOrWhiteSpace(result))
            {
                var index = result.IndexOf(":", StringComparison.InvariantCultureIgnoreCase);
                if (index > 0)
                    result = result.Substring(0, index);
            }

            return result;
        }

        private static bool IsRequestAvailable(HttpContext httpContext)
        {
            if (httpContext == null)
                return false;

            try
            {
                if (httpContext.Request == null)
                    return false;
            }
            catch (HttpException)
            {
                return false;
            }

            return true;
        }
    }
}
