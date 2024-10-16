using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.Manch;
using Project.Service.Models.Manch.Status;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Project.Service.Controllers.eSign
{
    public class SignDocumentController : ApiController
    {
        private const string FILE_DIRECTORY_NAME = "manch/{0}/upload/{1}";
        private readonly string _orgKey;
        private readonly string _secureKey;
        private readonly string _templateKey;
        private readonly string _documentType;
        private readonly string _baseUrl;
        private readonly string _returnUrl;

        public SignDocumentController()
        {
            _orgKey = ConfigurationManager.AppSettings["Manch.OrgKey"];
            _secureKey = ConfigurationManager.AppSettings["Manch.SecureKey"];
            _templateKey = ConfigurationManager.AppSettings["Manch.TemplateKey"];
            _documentType = ConfigurationManager.AppSettings["Manch.DocumentType"];
            _baseUrl = ConfigurationManager.AppSettings["Manch.BaseUrl"];
            _returnUrl = ConfigurationManager.AppSettings["Manch.ReturnUrl"];
        }


        [HttpPost]
        [ValidateModel]
        [Route("api/manch/ledger-sign-view")]
        public HttpResponseMessage LederSignViewDocument(SignLedgerInputModel signLedgerInputModel)
        {
            string data1 = string.Empty;
            var g1 = new DataConnectionTrans();
            var cm = new Common();
            if (DateTime.TryParse(signLedgerInputModel.FromDate, out DateTime fromDate)
                && DateTime.TryParse(signLedgerInputModel.ToDate, out DateTime toDate))
            {
                // TODO: Any further validation
                try
                {
                    var dt = g1.return_dt("getSaleLedgerBalance_Esign '" + signLedgerInputModel.CIN + "','" + signLedgerInputModel.FromDate + "','" + signLedgerInputModel.ToDate + "'");

                    List<SignLedgerOutputModelLINK> alldcr = new List<SignLedgerOutputModelLINK>();
                    List<SignLedgerOutputModel> result = new List<SignLedgerOutputModel>();


                    string _amount = "0";
                    string _link = "";
                    bool _isExist = false;

                    if (dt.Rows.Count > 0)
                    {
                        _amount = dt.Rows[0]["balance"].ToString();
                        _link = dt.Rows[0]["link"].ToString();
                        _isExist = Convert.ToBoolean(dt.Rows[0]["Isexist"]);

                    }



                    alldcr.Add(new SignLedgerOutputModelLINK
                    {
                        link = _link,
                        amount = _amount,
                        IsExists = _isExist
                    });


                    result.Add(new SignLedgerOutputModel
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = alldcr,
                    });


                    data1 = JsonConvert.SerializeObject(result, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                    return response;
                }
            }

            HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK);
            response1.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8,
                "application/json");

            return response1;
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/manch/ledger-sign-viewreport")]
        public HttpResponseMessage SignViewReportDocument(SignLedgerReportInputModel signLedgerReportInputModel)
        {
            string data1 = string.Empty;
            var cm = new Common();
            var g1 = new DataConnectionTrans();
            if (signLedgerReportInputModel.CIN != "")
            {
                // TODO: Any further validation
                try
                {

                    List<SignLedgerReportOutputModel> alldcr = new List<SignLedgerReportOutputModel>();
                    List<SignLedgerReport> alldcr1 = new List<SignLedgerReport>();
                    List<SignLedgerstatus> status = new List<SignLedgerstatus>();
                    List<SignLedgerReportData> data = new List<SignLedgerReportData>();
                    List<SignLedgerReportOutputModel> result = new List<SignLedgerReportOutputModel>();


                    var dr = g1.return_dt("esignledgerreport '" + signLedgerReportInputModel.CIN + "'");

                    var dr1 = g1.return_dt("esignledgerActive '" + signLedgerReportInputModel.CIN + "'");



                    bool isactive = false;

                    if (dr1.Rows.Count > 0)
                    { isactive = true; }


                    status.Add(new SignLedgerstatus
                    {
                        Isactive = isactive
                    });



                    if (dr.Rows.Count > 0)

                    {


                        for (int i = 0; i < dr.Rows.Count; i++)

                        {
                            alldcr1.Add(new SignLedgerReport
                            {
                                year = dr.Rows[i]["year"].ToString(),
                                quater = dr.Rows[i]["quater"].ToString(),
                                amount = dr.Rows[i]["amount"].ToString(),
                                link = dr.Rows[i]["link"].ToString(),

                            });

                        }

                        data.Add(new SignLedgerReportData
                        {
                            signdata = alldcr1,
                            status = status
                        });

                        result.Add(new SignLedgerReportOutputModel
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = data,
                        });


                        data1 = JsonConvert.SerializeObject(result, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;

                    }
                    else
                    {


                        data.Add(new SignLedgerReportData
                        {
                            signdata = alldcr1,
                            status = status
                        });


                        result.Add(new SignLedgerReportOutputModel
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = data,
                        });


                        data1 = JsonConvert.SerializeObject(result, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

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

            HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK);
            response1.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8,
                "application/json");

            return response1;
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/manch/make-payload-to-sign")]
        public HttpResponseMessage MakePayloadToSignDocument(SignDocumentInputModel signDocumentInputModel)
        {
            var blobFilePath = string.Empty;

            var cm = new Common();

            //if (blobFileName == "")
            //{
            //    HttpResponseMessage response4 = Request.CreateResponse(HttpStatusCode.OK);
            //    response4.Content = new StringContent(cm.StatusTime(false, "File Not Found !!!!!!!!"), Encoding.UTF8, "application/json");

            //    return response4;
            //}



            if (DateTime.TryParse(signDocumentInputModel.FromDate, out DateTime fromDate)
                && DateTime.TryParse(signDocumentInputModel.ToDate, out DateTime toDate))

            //signDocumentInputModel.FromDate = "2024-04-01";
            //string fromDate = signDocumentInputModel.FromDate;

            //signDocumentInputModel.ToDate = "2024-06-30";
            //string toDate = signDocumentInputModel.ToDate;

            //if (signDocumentInputModel.CIN != "")
            {
                // TODO: Any further validation
                try
                {
                    var blobFileName = $"PartyLedger_{signDocumentInputModel.CIN}_{DateTime.Now:dd_MMM_yyy_hh_mm_ss_fff}.pdf";

                    var g1 = new DataConnectionTrans();

                    var BLOB_DIRECTORY_NAME =
                        string.Format(FILE_DIRECTORY_NAME, signDocumentInputModel.CIN, DateTime.Now.ToString("dd-mm-yyyy"));

                    using (PartyLedgerReportNew report = new PartyLedgerReportNew())
                    {
                        report.Parameters["parameter3"].Value = signDocumentInputModel.CIN;
                        //report.Parameters["parameter4"].Value = signDocumentInputModel.FromDate;
                        //report.Parameters["parameter5"].Value = signDocumentInputModel.ToDate;
                        report.Parameters["parameter4"].Value = $"{fromDate:MM/dd/yyyy}";
                        report.Parameters["parameter5"].Value = $"{toDate:MM/dd/yyyy}";

                        blobFilePath = ToPrintReport(report, blobFileName
                            ,
                            BLOB_DIRECTORY_NAME);
                    }

                    if (!string.IsNullOrWhiteSpace(blobFilePath))
                    {
                        var ledgerTable = g1.return_dt(
                             $"exec SavePartyLedgerInfoForSign '{signDocumentInputModel.CIN}','{blobFilePath}','{fromDate:yyyy-MM-dd}','{toDate:yyyy-MM-dd}'");
                        //$"exec SavePartyLedgerInfoForSign '{signDocumentInputModel.CIN}','{blobFilePath}','{signDocumentInputModel.FromDate:yyyy-MM-dd}','{signDocumentInputModel.ToDate:yyyy-MM-dd}'");
                        if (ledgerTable.Rows.Count > 0)
                        {
                            var firstName = ledgerTable.Rows[0]["FirstName"].ToString();
                            var lastName = ledgerTable.Rows[0]["LastName"].ToString();
                            var mobileNumber = "7208301119"; // ledgerTable.Rows[0]["MobileNumber"].ToString();
                            var email = "chetan.goldmedalindia@gmail.com"; // ledgerTable.Rows[0]["Email"].ToString();
                            var requestId = ledgerTable.Rows[0]["RequestId"].ToString();

                            var documents = new Models.Manch.Document
                            {
                                documentType = _documentType,
                                documentTypeUrl = blobFilePath,
                            };

                            var manchPayload = new ManchPayload
                            {
                                firstName = firstName,
                                lastName = lastName,
                                templateKey = _templateKey,
                                mobileNumber = mobileNumber,
                                email = email,
                                returnURL = _returnUrl,
                                documents = new List<Models.Manch.Document> { documents }
                            };

                            var token = GenerateHMACSHA256(requestId);
                            using (var client = new HttpClient())
                            {
                                //client.BaseAddress = new Uri(_baseUrl);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Add("AUTHORIZATION", $"HS256 {_orgKey}:{token}");
                                client.DefaultRequestHeaders.Add("REQUEST-ID", requestId);
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                using (var response = client.PostAsJsonAsync(_baseUrl + "/api/transactions", manchPayload).Result)
                                {
                                    var data = response.Content.ReadAsStringAsync().Result;
                                    using (var stream = response.Content.ReadAsStreamAsync().Result)
                                    {
                                        if (response.IsSuccessStatusCode)
                                        {
                                            var manchTransactionResponse =
                                                DeserializeJsonFromStream<ManchTransactionResponse>(stream);

                                            if (manchTransactionResponse.responseCode == "1" &&
                                                manchTransactionResponse.requestId == requestId)
                                            {
                                                //TODO: save the response from manch
                                                var result = g1.ExecDB(
                                                                    $"exec UpdatePartyLedgerInfoForSign '{requestId}'," +
                                                                    $"'{manchTransactionResponse.data.transaction.transactionId}'," +
                                                                    $"'{manchTransactionResponse.data.documents[0].documentId}'," +
                                                                    $"'{manchTransactionResponse.data.documents[0].documentLink}'");
                                                //Send the document link to app as response

                                                string data1;
                                                List<ManchResponseToClientApps> alldcr = new List<ManchResponseToClientApps>();
                                                List<ManchResponseToClientApp> alldcr1 = new List<ManchResponseToClientApp>();

                                                alldcr1.Add(new ManchResponseToClientApp
                                                {
                                                    hashToken = $"HS256 {_orgKey}:{token}",
                                                    requestId = requestId,
                                                    documentLink = manchTransactionResponse.data.documents[0].documentLink
                                                });
                                                alldcr.Add(new ManchResponseToClientApps
                                                {
                                                    result = true,
                                                    message = string.Empty,
                                                    servertime = DateTime.Now.ToString(),
                                                    data = alldcr1,
                                                });
                                                data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                                HttpResponseMessage response2 = Request.CreateResponse(HttpStatusCode.OK);

                                                response2.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                                                return response2;
                                            }
                                        }

                                        var content = StreamToStringAsync(stream).Result;
                                        var statusCode = response.StatusCode;

                                        HttpResponseMessage response3 = Request.CreateResponse(HttpStatusCode.OK);
                                        response3.Content = new StringContent(cm.StatusTime(false, $"Status Code:{statusCode}  Content:{content}"), Encoding.UTF8, "application/json");

                                        return response3;
                                    }
                                }
                            }
                        }
                        else
                        {
                            HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK);
                            response1.Content = new StringContent(cm.StatusTime(false, "ledger table have no value"), Encoding.UTF8,
                                "application/json");

                            return response1;
                        }
                    }

                    else
                    {
                        HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK);
                        response1.Content = new StringContent(cm.StatusTime(false, "Blob Error"), Encoding.UTF8,
                            "application/json");

                        return response1;
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
                HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8,
                    "application/json");

                return response1;

            }

        }

        [HttpPost]
        [ValidateModel]
        [Route("api/verify-sign-document")]
        public HttpResponseMessage VerifyAndDownloadManchDocument(StatusAckInputModel statusAckInputModel)
        {
            var g1 = new DataConnectionTrans();
            var cm = new Common();
            var errorlog = "";
            if (statusAckInputModel.CIN != "")
            {
                try
                {
                    //Step-1 check if there is status success
                    var ledgerTable = g1.return_dt(
                           $"exec VerifyPartyLedgerInfoForSign '{statusAckInputModel.CIN}','{statusAckInputModel.RequestId}','{statusAckInputModel.StatusCode}'");
                    if (ledgerTable.Rows.Count > 0)
                    {
                        var transactionId = ledgerTable.Rows[0]["TransactionId"].ToString();
                        var documentId = ledgerTable.Rows[0]["DocumentId"].ToString();
                        var documentLink = ledgerTable.Rows[0]["DocumentLink"].ToString();
                        //GET transaction status

                        var token = GenerateHMACSHA256(statusAckInputModel.RequestId);
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Add("AUTHORIZATION", $"HS256 {_orgKey}:{token}");
                            client.DefaultRequestHeaders.Add("REQUEST-ID", statusAckInputModel.RequestId);
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                            using (var response = client.GetAsync($"{_baseUrl}/api/transactions/{transactionId}/status").Result)
                            {

                                using (var stream = response.Content.ReadAsStreamAsync().Result)
                                {

                                    if (response.IsSuccessStatusCode)
                                    {

                                        var manchTransactionResponse =
                                             DeserializeJsonFromStream<TransactionStatus>(stream);

                                        if (manchTransactionResponse.responseCode == "1" &&
                                            manchTransactionResponse.requestId == statusAckInputModel.RequestId)
                                        {

                                            var NewDocID = g1.reterive_val(
                                                              $"exec UpdateNewDocumentidForSign '{statusAckInputModel.RequestId}'," +
                                                              $"'{manchTransactionResponse.data.documents[0].documentURL}'");

                                            var token1 = GenerateHMACSHA256(statusAckInputModel.RequestId);


                                            //Download and save document
                                            using (var client1 = new HttpClient())
                                            {
                                                client1.DefaultRequestHeaders.Accept.Clear();
                                                client1.DefaultRequestHeaders.Add("AUTHORIZATION", $"HS256 {_orgKey}:{token1}");
                                                client1.DefaultRequestHeaders.Add("REQUEST-ID", statusAckInputModel.RequestId);
                                                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                                //using (var ms = client1.GetStreamAsync($"{_baseUrl}/api/documents/{manchTransactionResponse.data.documents[0].documentStorageId}/content").Result)

                                                //if (client1.GetAsync($"{_baseUrl}/api/documents/{documentId}/content").Result.IsSuccessStatusCode)
                                                //{
                                                // var ms = client1.GetStreamAsync($"{_baseUrl}/api/documents/{documentId}/content").Result;
                                                //  var ms2 = client1.GetStreamAsync($"{documentLink}/sign-url").Result

                                                using (var ms = client1.GetStreamAsync($"{_baseUrl}/api/documents/{NewDocID}/content").Result)
                                                {
                                                    {
                                                        var msD = DeserializeJsonFromStream<eSigned>(client1.GetAsync($"{documentLink}/sign-url").Result.Content.ReadAsStreamAsync().Result);

                                                        //var saveoutput = g1.return_dt($"exec updatePartyLedgerInfoForSignOutput '{statusAckInputModel.CIN}','{statusAckInputModel.RequestId}','{ttt.requestId+","+ttt.responseCode + "," + ttt.data.signedurl}'");
                                                        var saveoutput = g1.return_dt($"exec updatePartyLedgerInfoForSignOutput '{statusAckInputModel.CIN}','{statusAckInputModel.RequestId}','{ msD.data.signURL}'");
                                                        //TODO: Upload to blob
                                                        var BLOB_DIRECTORY_NAME =
                                                        $"{string.Format(FILE_DIRECTORY_NAME, statusAckInputModel.CIN, DateTime.Now.ToString("dd-mm-yyyy"))}/signed";

                                                        var _goldMedia = new GoldMedia();
                                                        Dictionary<bool, string> retStr;

                                                        retStr = _goldMedia.GoldMediaUpload(manchTransactionResponse.data.documents[0].documentType + DateTime.Now.ToString("HHmmss"), BLOB_DIRECTORY_NAME, string.Empty, ms,
                                                            "application/pdf", false);
                                                        if (retStr.Keys.FirstOrDefault())
                                                        {
                                                            var filename = _goldMedia.MapPathToPublicUrl(retStr.Values.FirstOrDefault());

                                                            var result = 0;

                                                            if (manchTransactionResponse.data.documents[0].signerInfo.Count > 0)

                                                            {
                                                                result = g1.ExecDB(
                                                           $"exec UpdateStatusPartyLedgerInfoForSign '{statusAckInputModel.RequestId}'," +
                                                           $"'{manchTransactionResponse.data.transactionState}'," +
                                                           $"'{manchTransactionResponse.data.documents[0].documentURL}'," +


                                                                   $"'{manchTransactionResponse.data.documents[0].signerInfo[0].commonName}'," +
                                                                   $"'{manchTransactionResponse.data.documents[0].signerInfo[0].title}'," +
                                                                   $"'{manchTransactionResponse.data.documents[0].signerInfo[0].yob}'," +
                                                                   $"'{manchTransactionResponse.data.documents[0].signerInfo[0].gender}'," +

                                                           $"'{manchTransactionResponse.data.documents[0].signed}'," +
                                                           $"'{filename}'");
                                                            }
                                                            else
                                                            {
                                                                result = g1.ExecDB(
                                                           $"exec UpdateStatusPartyLedgerInfoForSign '{statusAckInputModel.RequestId}'," +
                                                           $"'{manchTransactionResponse.data.transactionState}'," +
                                                           $"'{manchTransactionResponse.data.documents[0].documentURL}'," + "'','','',''," +

                                                           //$"'{manchTransactionResponse.data.documents[0].signerInfo[0].commonName}'," + 
                                                           //$"'{manchTransactionResponse.data.documents[0].signerInfo[0].title}'," +
                                                           //$"'{manchTransactionResponse.data.documents[0].signerInfo[0].yob}'," +
                                                           //$"'{manchTransactionResponse.data.documents[0].signerInfo[0].gender}'," +

                                                           $"'{manchTransactionResponse.data.documents[0].signed}'," +
                                                           $"'{filename}'");
                                                            }

                                                            //Send the document link to app as response

                                                            string data1;
                                                            List<ManchResponseToClientApps> alldcr = new List<ManchResponseToClientApps>();
                                                            List<ManchResponseToClientApp> alldcr1 = new List<ManchResponseToClientApp>();


                                                            var AadharNoMatch = g1.return_dt($"exec GetAadharNoOutput '{statusAckInputModel.CIN}'");
                                                            string aadharNo = AadharNoMatch.Rows[0]["AadharCardNoFourDigit"].ToString();

                                                            if (manchTransactionResponse.data.documents[0].signerInfo[0].title.ToString() == aadharNo.ToString())
                                                            {
                                                                alldcr1.Add(new ManchResponseToClientApp
                                                                {
                                                                    hashToken = token,
                                                                    requestId = statusAckInputModel.RequestId,
                                                                    documentLink = filename
                                                                });

                                                                alldcr.Add(new ManchResponseToClientApps
                                                                {
                                                                    result = true,
                                                                    message = string.Empty,
                                                                    servertime = DateTime.Now.ToString(),
                                                                    data = alldcr1,
                                                                });
                                                                data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                                                                HttpResponseMessage response2 = Request.CreateResponse(HttpStatusCode.OK);

                                                                response2.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                                                                return response2;

                                                            }
                                                            else
                                                            {
                                                                HttpResponseMessage response4 = Request.CreateResponse(HttpStatusCode.OK);
                                                                response4.Content = new StringContent(cm.StatusTime(false, "Invalid Aadhar Card!!!!!"), Encoding.UTF8, "application/json");

                                                                return response4;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            HttpResponseMessage response4 = Request.CreateResponse(HttpStatusCode.OK);
                                                            response4.Content = new StringContent(cm.StatusTime(false, "Oops! Somthing went wrong!!!!!"), Encoding.UTF8, "application/json");

                                                            return response4;
                                                        }
                                                    }
                                                }
                                                //}

                                                //else
                                                //{
                                                //    HttpResponseMessage response4 = Request.CreateResponse(HttpStatusCode.OK);
                                                //    response4.Content = new StringContent(cm.StatusTime(false, "Oops! Somthing went wrong or duplicate Request!!!!!"), Encoding.UTF8, "application/json");

                                                //    return response4;
                                                //}

                                            }
                                        }

                                        else
                                        {
                                            HttpResponseMessage response4 = Request.CreateResponse(HttpStatusCode.OK);
                                            response4.Content = new StringContent(cm.StatusTime(false, "Oops! Somthing went wrong!!!!!"), Encoding.UTF8, "application/json");

                                            return response4;
                                        }
                                    }

                                    else
                                    {
                                        HttpResponseMessage response4 = Request.CreateResponse(HttpStatusCode.OK);
                                        response4.Content = new StringContent(cm.StatusTime(false, "Oops! Somthing went wrong!!!!!" + response.IsSuccessStatusCode.ToString()), Encoding.UTF8, "application/json");

                                        return response4;
                                    }

                                    //// var content = StreamToStringAsync(response.Content.ReadAsStreamAsync().Result).Result;
                                    //var statusCode = response.StatusCode;

                                    //HttpResponseMessage response3 = Request.CreateResponse(HttpStatusCode.OK);
                                    //response3.Content = new StringContent(cm.StatusTime(false, $"Status Code:{statusCode}"), Encoding.UTF8, "application/json");

                                    //return response3;
                                }
                            }
                        }
                        //

                    }
                    else
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Not a Valid Transaction!!!!!"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
            //HttpResponseMessage response5 = Request.CreateResponse(HttpStatusCode.OK);
            //response5.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

            //return response5;
        }

        private static string GetFilePath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }

        private string ToPrintReport(XtraReport report, string tempFileName, string directory)
        {
            string filename = string.Empty;

            using (var ms = new MemoryStream())
            {
                report.CreateDocument();
                var opts = new PdfExportOptions
                {
                    ShowPrintDialogOnOpen = false
                };
                report.ExportToPdf(ms, opts);
                ms.Seek(0, SeekOrigin.Begin);

                var _goldMedia = new GoldMedia();
                Dictionary<bool, string> retStr;

                retStr = _goldMedia.GoldMediaUpload(tempFileName, directory, string.Empty, ms,
                    "application/pdf", false);
                if (retStr.Keys.FirstOrDefault())
                {
                    filename = _goldMedia.MapPathToPublicUrl(retStr.Values.FirstOrDefault());
                }
            }

            return filename;
        }

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                {
                    content = await sr.ReadToEndAsync().ConfigureAwait(false);
                }

            return content;
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream?.CanRead != true)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        private string GenerateHMACSHA256(string requestId)
        {
            var message = Encoding.UTF8.GetBytes(_orgKey + requestId);
            var key = Encoding.UTF8.GetBytes(_secureKey);

            var hashString = new HMACSHA256(key);
            //var hashString = new SHA512Managed();
            var hashValue = hashString.ComputeHash(message);

            return hashValue.Aggregate("", (current, x) => current + $"{x:x2}");
        }

        //private
    }

    //TODO:
    //step-1: Create transaction in manch server and send token,documentlink and requestid
    //Step-2: In successfull acknowledge create an api to get document, and verify name and title then download sign document and persists and send ack to app
    //Optional(delete doc after download and save in our server)
}