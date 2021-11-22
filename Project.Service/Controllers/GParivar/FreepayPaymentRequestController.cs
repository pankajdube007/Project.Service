using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NLog;

namespace Project.Service.Controllers
{
    
    public class FreepayPaymentRequestController : ApiController
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        [HttpPost]
        [ValidateModel]
        [Route("api/PaymentFreepayRequest")]
        public HttpResponseMessage GetDetails(ListofFreepayPaymentRequest ula)
        {
            Common cm = new Common();
            string data1;
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;
            if (ula.CIN != "" && ula.payBankCode != "" && ula.transactionid != "")
            {
                try
                {

                    PaymentInfo paymentInfos = new PaymentInfo();
                    List<Invoice1> invoicess = new List<Invoice1>();
                    List<CreditNote> CreditNotess = new List<CreditNote>();
                    List<FreepayPaymentRequest> dealerdetails = new List<FreepayPaymentRequest>();
                    List<FreepayPaymentRequests> dealerdetailss = new List<FreepayPaymentRequests>();
                    RootObject RootObjects = new RootObject();



                    var dr = g2.return_dt("FreePayPaymentRequest '" + ula.transactionid + "'");

                    if (dr.Rows.Count > 0)
                    {

                        paymentInfos.payId = dr.Rows[0]["TRANSID"].ToString();
                        paymentInfos.payAmount = Convert.ToDouble(dr.Rows[0]["distotal"]);
                        paymentInfos.payBankCode = ula.payBankCode.ToString();
                        paymentInfos.payDate = dr.Rows[0]["payDate"].ToString();

                        var dr1 = g2.return_dr("FreePayPaymentRequestChild " + Convert.ToInt32(dr.Rows[0]["slno"]));

                        if (dr1.HasRows)
                        {

                        

                            while (dr1.Read())
                            {
                                invoicess.Add(new Invoice1
                                {
                                    paydocNumber = dr1["invoice"].ToString(),
                                    paydocType = dr1["catid"].ToString(),
                                    paydocDueDate = dr1["paydocDueDate"].ToString(),
                                    paydocOriginalAmount = Convert.ToDouble(dr1["enteredamt"]),
                                    paydocOutstandingAmount = Convert.ToDouble(dr1["prevbalamount"].ToString()),
                                    paydocCdApplied = Convert.ToDouble(dr1["CDAMOUNT"].ToString()),
                                    paydocPayAmount = Convert.ToDouble(dr1["adjustedamount"].ToString()),
                                    creditNotes = CreditNotess
                                }
                                    );

                            }

                        }


                        RootObjects.paymentInfo = paymentInfos;
                        RootObjects.invoices = invoicess;





                        Dictionary<string, string> header = new Dictionary<string, string>
                    {
                        { "x-cpg-code", ConfigurationManager.AppSettings["Gold.Freepay.Code"] },
                        { "x-dealer-code", ula.CIN.ToString() },
                        //{ "Accept", "application/json" },
                        //{ "Content-type", "application/json" }
                    };


                        //   var test = JsonConvert.SerializeObject(RootObjects);

                        var baseurl = ConfigurationManager.AppSettings["Gold.Freepay.API"] + "payments";
                        RemoteStatus res;
                        using (var remoteClient = new RemoteClient())
                        {
                            res = remoteClient.PostAsync(url: baseurl,
                               requestParam: RootObjects,
                               customsHeader: header).Result;
                        }

                        Logger.Info(res.Response);
                        dynamic _output = JsonConvert.DeserializeObject(res.Response);
                        //JToken value;
                        //var sucessstatus = _output.TryGetValue("status", out value);
                        //dynamic _output = JObject.Parse(res);
                        //var _outputstatus = _output.status.code.ToString();

                        //    output.status.code
                        var inputstr = JsonConvert.SerializeObject(RootObjects);

                        var dr55 = g2.return_dt("updatefreepaypaymentoutput '" + res.Response .ToString()+ "','" + ula.transactionid + "','"+ inputstr.ToString()+"'");

                        if (res.StatusCode == 200)
                        {

                            if (_output.status.code == "200")
                            {
                                if (ula.transactionid != "" && _output.data.freepayTxnId != "" && _output.data.freepayTxnId != null)
                                { 
                                    var dr2 = g2.return_dr("FreePayPaymentRequestTransupdate '" + ula.transactionid + "','" + _output.data.freepayTxnId + "','"+ula.payBankCode+"'");

                                    if (dr2.HasRows)
                                    {

                                        while (dr2.Read())
                                        {
                                            dealerdetails.Add(new FreepayPaymentRequest
                                            {
                                                mobile = Convert.ToString(dr2["mobile"].ToString()),
                                                email = Convert.ToString(dr2["email"].ToString()),
                                            });
                                        }

                                        g2.close_connection();
                                        dealerdetailss.Add(new FreepayPaymentRequests
                                        {
                                            result = true,
                                            message = string.Empty,
                                            servertime = DateTime.Now.ToString(),
                                            data = dealerdetails,
                                        });
                                        data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                                        return response;
                                    }
                                    else
                                    {
                                        dealerdetails.Add(new FreepayPaymentRequest
                                        {
                                            mobile = "",
                                            email = "",
                                        });


                                        g2.close_connection();

                                        dealerdetailss.Add(new FreepayPaymentRequests
                                        {
                                            result = false,
                                            message = "Oops! TransactionId or FreePay ID Missing!!!!!!!!",
                                            servertime = DateTime.Now.ToString(),
                                            data = dealerdetails,
                                        });
                                        data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                                        return response;

                                    }
                                }

                                else
                                {
                                    dealerdetails.Add(new FreepayPaymentRequest
                                    {
                                        mobile = "",
                                        email = "",
                                    });


                                    g2.close_connection();

                                    dealerdetailss.Add(new FreepayPaymentRequests
                                    {
                                        result = false,
                                        message = "Oops! TransactionId not Correct!!!!!!!!",
                                        servertime = DateTime.Now.ToString(),
                                        data = dealerdetails,
                                    });
                                    data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                                    return response;

                                   
                                }
                            }


                            else
                            {

                                dealerdetails.Add(new FreepayPaymentRequest
                                {
                                    mobile = "",
                                    email = "",
                                });

                                g2.close_connection();
                                dealerdetailss.Add(new FreepayPaymentRequests
                                {
                                    result = false,
                                    message = "Oops! Input is not in proper format!!!!!!!!",
                                    servertime = DateTime.Now.ToString(),
                                    data = dealerdetails,
                                });
                                data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                                response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                                return response;

                            
                            }



                        }

                        else if (res.StatusCode == 400 && _output.status.errorCode.ToString() == "F111")
                        {

                            dealerdetails.Add(new FreepayPaymentRequest
                            {
                                mobile = "",
                                email = "",
                            });

                            g2.close_connection();
                            dealerdetailss.Add(new FreepayPaymentRequests
                            {
                                result = false,
                                message = "Oops! Invalid bank details!!!!!!!!",
                                servertime = DateTime.Now.ToString(),
                                data = dealerdetails,
                            });
                            data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                            return response;

                                                        
                        }

                        else if (res.StatusCode == 400 && _output.status.errorCode.ToString() == "F112")
                        {

                            dealerdetails.Add(new FreepayPaymentRequest
                            {
                                mobile = "",
                                email = "",
                            });

                            g2.close_connection();
                            dealerdetailss.Add(new FreepayPaymentRequests
                            {
                                result = false,
                                message = "Oops! Mandate amount limit restriction.!!!!!!!!",
                                servertime = DateTime.Now.ToString(),
                                data = dealerdetails,
                            });
                            data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                            return response;


                        }

                        else if (res.StatusCode == 400 && _output.status.errorCode.ToString() == "F114")
                        {
                            dealerdetails.Add(new FreepayPaymentRequest
                            {
                                mobile = "",
                                email = "",
                            });

                            g2.close_connection();
                            dealerdetailss.Add(new FreepayPaymentRequests
                            {
                                result = false,
                                message = "Oops! UMRN number does not belong to dealer for which payment is been initiated.!!!!!!!!",
                                servertime = DateTime.Now.ToString(),
                                data = dealerdetails,
                            });
                            data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                            return response;


                        }

                        else if (res.StatusCode == 400 && _output.status.errorCode.ToString() == "F012")
                        {
                            dealerdetails.Add(new FreepayPaymentRequest
                            {
                                mobile = "",
                                email = "",
                            });

                            g2.close_connection();
                            dealerdetailss.Add(new FreepayPaymentRequests
                            {
                                result = false,
                                message = "Oops! User Not Registered.!!!!!!!!",
                                servertime = DateTime.Now.ToString(),
                                data = dealerdetails,
                            });
                            data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                            return response;
                            
                        }


                        else
                        {

                            dealerdetails.Add(new FreepayPaymentRequest
                            {
                                mobile = "",
                                email = "",
                            });

                            g2.close_connection();
                            dealerdetailss.Add(new FreepayPaymentRequests
                            {
                                result = false,
                                message = "Oops! Payment Request Not Complete, may be already exists, try after some time!!!!!!!!",
                                servertime = DateTime.Now.ToString(),
                                data = dealerdetails,
                            });
                            data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                            return response;

                        }


                    }
                    else
                    {
                        dealerdetails.Add(new FreepayPaymentRequest
                        {
                            mobile = "",
                            email = "",
                        });

                        g2.close_connection();
                        dealerdetailss.Add(new FreepayPaymentRequests
                        {
                            result = false,
                            message = "Oops! TransactionId not Correct!!!!!!!!",
                            servertime = DateTime.Now.ToString(),
                            data = dealerdetails,
                        });
                        data1 = JsonConvert.SerializeObject(dealerdetailss, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                        return response;
                        
                    }

                }
                catch (Exception ex)
                {


                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! TransactionId not Correct!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid data."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}