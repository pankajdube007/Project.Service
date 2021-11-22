using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Project.Service.Citi;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.Citi;
using Raptorious.SharpMt940Lib;
using Raptorious.SharpMt940Lib.Mt940Format;

namespace Project.Service.Controllers.Citi
{
    public class CitiPaymentController : ApiController
    {
        private const string LocalBaseDirectory = "~/App_Data/citi/upload";
        private const string GeplTransactionFilename = "gepl-transaction";
        private const string FILE_DIRECTORY_NAME = "citi/upload";
        private const string LocalBaseDownloadDirectory = "~/App_Data/citi/download/transaction";

        [HttpPost]
        [ValidateModel]
        [Route("api/citi/make-payload")]
        public HttpResponseMessage CitiPayload()
        {
            var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
            var cm = new Common();
            try
            {
                var g1 = new DataConnectionTrans();

                //Step-1 get all data
                var getAllPendingPayment = g1.return_dt("exec getALLPendingPayment");

                if (getAllPendingPayment.Rows.Count > 0)
                {
                    var fileName =
                        $"{GeplTransactionFilename}-{DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss_tt").ToLower()}.txt";
                    var fileFullNameWithPath = $"{LocalBaseDirectory}/transaction/{fileName}";
                    var getFilePath = GetFilePath(fileFullNameWithPath);

                    //Save to local
                    var filePath = DataTableToCsv(getAllPendingPayment, getFilePath);

                    if (File.Exists(filePath))
                    {
                        //Step-2:Save to blob
                        var _goldMedia = new GoldMedia();
                        Dictionary<bool, string> retStr;

                        using (Stream fileStream = new FileStream(getFilePath, FileMode.Open))
                        {
                            retStr = _goldMedia.GoldMediaUpload(fileName, $"{FILE_DIRECTORY_NAME}/transaction", "",
                                fileStream,
                                "text/plain", false);
                        }

                        if (retStr.Keys.FirstOrDefault())
                        {
                            var blobfilename = _goldMedia.MapPathToPublicUrl(retStr.Values.FirstOrDefault());

                            //Step-3
                            g1.ExecDB(
                                $"exec CitiFtpPaymentJobsAddStep1 '{fileFullNameWithPath}','{blobfilename}','{fileName}'");

                            httpResponseMessage.Content = new StringContent(
                                JsonConvert.SerializeObject(getAllPendingPayment),
                                Encoding.UTF8, "application/json");
                            return httpResponseMessage;
                        }

                        g1.close_connection();

                        httpResponseMessage.Content = new StringContent(
                            cm.StatusTime(false, "Unable to upload file to blob"),
                            Encoding.UTF8, "application/json");
                        return httpResponseMessage;
                    }

                    g1.close_connection();

                    httpResponseMessage.Content =
                        new StringContent(cm.StatusTime(false, "Unable to generate file in local storage"),
                            Encoding.UTF8, "application/json");
                    return httpResponseMessage;
                }

                g1.close_connection();

                httpResponseMessage.Content = new StringContent(cm.StatusTime(false, "No Data available"),
                    Encoding.UTF8,
                    "application/json");
                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                httpResponseMessage.Content =
                    new StringContent(
                        cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message),
                        Encoding.UTF8, "application/json");
                return httpResponseMessage;
            }
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/citi/payout/status")]
        public HttpResponseMessage CitiPayoutStatusSync()
        {
            var cm = new Common();
            try
            {
                var row = 0;
                var actualFileName = string.Empty;
                var payoutTransactions = new List<CitiPayoutTransaction>();
                var goldCitiBanks = new List<GoldCitiBank>();

                var g1 = new DataConnectionTrans();
                ICitiPayment remoteClient = new CitiPayment();

                var downloadedFile = remoteClient.DownloadCitiBankTransactionResponseFile();

                //var downloadedFile = "MT940_H2H_202109202131";
                var fileStringDate = downloadedFile.Substring(downloadedFile.LastIndexOf('_') + 1, 8);
                var fileDate =
                    Convert.ToDateTime(
                        $"{fileStringDate.Substring(0, 4)}, {fileStringDate.Substring(4, 2)}, {fileStringDate.Substring(6, 2)}");

                if (!string.IsNullOrWhiteSpace(downloadedFile))
                {
                    actualFileName = $"{downloadedFile}.txt";
                    var filePath = GetFilePath($"{LocalBaseDownloadDirectory}/{actualFileName}");

                    var header = new Separator("");
                    var footer = new Separator("-");
                    var format = new GenericFormat(header, footer);

                    var customerStatementMessagesList = GetParsedTransactionFile(format, filePath);
                    var customerStatementMessages = customerStatementMessagesList.Skip(0).Take(1).FirstOrDefault();
                    var transactionList = customerStatementMessages?.Transactions;


                    if (transactionList != null)
                        foreach (var trans in transactionList)
                            if (trans.Reference.Contains("GEPL"))
                                payoutTransactions.Add(new CitiPayoutTransaction
                                {
                                    Amount = $"{trans.Amount.Currency.Code} {trans.Amount.Value}",
                                    DebitCredit = trans.DebitCredit.ToString(),
                                    Description = trans.Description,
                                    CitiUTR = trans.Description.Split(' ').FirstOrDefault(a=>a.Contains("CITI")),
                                    Remarks = trans.Description.Replace("\r","").Split('\n')[1].Trim().Replace("?21","").Replace("?22","").Replace("/BN/"," ").Replace("/BI/"," "),
                                    TransferType = trans.Description.Split(' ')[0].Substring(6, 3),
                                    EntryDate = trans.EntryDate,
                                    GoldmedalTransactionReferenceId = trans.Reference.Substring(4),
                                    FundsCode = trans.FundsCode,
                                    AccountServicingReference = trans.AccountServicingReference,
                                    Account = trans.Details.Account,
                                    DetailsDescription = trans.Details.Description,
                                    Name = trans.Details.Name,
                                    TransactionType = trans.TransactionType,
                                    //Value = trans.Value,
                                    ValueDate = trans.ValueDate
                                });

                    goldCitiBanks.Add(new GoldCitiBank
                    {
                        AccountNo = customerStatementMessages?.Account,
                        OpeningBalance =
                            $"{customerStatementMessages?.OpeningBalance.Currency.Code} {customerStatementMessages?.OpeningBalance.Balance.Value}",
                        ClosingBalance =
                            $"{customerStatementMessages?.ClosingBalance.Currency.Code} {customerStatementMessages?.ClosingBalance.Balance.Value}",
                        StatementDate = fileDate,
                        CitiPayoutTransactions = payoutTransactions
                    });


                    foreach (var ben in payoutTransactions)
                    {
                        row = g1.ExecDB(
                            $"exec paymentStatusUpdate '{ben.GoldmedalTransactionReferenceId}','{ben.Amount}','{ben.CitiUTR}','{ben.ValueDate:yyyy-MM-dd}','{ben.Remarks}'");
                        if (row > 0) row++;
                    }


                    if (row > 0)
                    {
                        //Step-2:Save to blob
                        var _goldMedia = new GoldMedia();
                        Dictionary<bool, string> retStr;
                        var getFilePath = GetFilePath($"{LocalBaseDownloadDirectory}/{actualFileName}");
                        using (Stream fileStream = new FileStream(getFilePath, FileMode.Open))
                        {
                            retStr = _goldMedia.GoldMediaUpload(actualFileName,
                                $"{FILE_DIRECTORY_NAME}/transaction/status",
                                "",
                                fileStream,
                                "text/plain", false);
                        }

                        if (retStr.Keys.FirstOrDefault())
                        {
                            var blobfilename = _goldMedia.MapPathToPublicUrl(retStr.Values.FirstOrDefault());

                            g1.ExecDB(
                                $"exec PaymentStatusFileUpdate '{blobfilename}','{actualFileName}','{fileDate:yyyy-MM-dd}'");
                        }
                    }
                }

                var statement = JsonConvert.SerializeObject(goldCitiBanks);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content =
                    new StringContent(statement,
                        Encoding.UTF8, "application/json");

                return response;
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

        [HttpPost]
        [ValidateModel]
        [Route("api/citi/payout")]
        public HttpResponseMessage CitiPayoutSync()
        {
            HttpResponseMessage httpResponseMessage;
            var cm = new Common();
            try
            {
                var g1 = new DataConnectionTrans();
                ICitiPayment remoteClient = new CitiPayment();

                var getAllPendingPayment = g1.return_dt("exec getaAllFileTobeUpload");

                if (getAllPendingPayment.Rows.Count > 0)
                {
                    var _goldMedia = new GoldMedia();

                    var fileName = getAllPendingPayment.Rows[0]["FileName"].ToString();
                    var remoteFileNameAndPath =
                        $"{FILE_DIRECTORY_NAME}/transaction/{fileName}";
                    var fileFullNameWithPath = $"{LocalBaseDirectory}/transaction/temp/{fileName}";


                    var fileStream = _goldMedia.GoldMediaDownload(remoteFileNameAndPath);

                    using (var ms = new MemoryStream())
                    {
                        byte[] fileBytes = null;
                        var buffer = new byte[4096];
                        var chunkSize = 0;

                        do
                        {
                            chunkSize = fileStream.Read(buffer, 0, buffer.Length);
                            ms.Write(buffer, 0, chunkSize);
                        } while (chunkSize != 0);

                        fileStream.Close();
                        File.WriteAllBytes(GetFilePath(fileFullNameWithPath), ms.ToArray());
                    }

                    if (File.Exists(GetFilePath(fileFullNameWithPath)))
                    {
                        remoteClient.UploadPaymentTransaction(GetFilePath(fileFullNameWithPath), fileName);

                        var result = g1.ExecDB("exec CitiFtpPaymentJobsAddStep2 " +
                                               getAllPendingPayment.Rows[0]["Id"]);
                        httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);

                        httpResponseMessage.Content = new StringContent(
                            JsonConvert.SerializeObject(getAllPendingPayment),
                            Encoding.UTF8, "application/json");

                        return httpResponseMessage;
                    }

                    g1.close_connection();
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StringContent(
                        cm.StatusTime(false, "No File exists to as no Data available"),
                        Encoding.UTF8,
                        "application/json");

                    return httpResponseMessage;
                }

                g1.close_connection();
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                httpResponseMessage.Content = new StringContent(cm.StatusTime(false, "No Data available"),
                    Encoding.UTF8,
                    "application/json");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                httpResponseMessage.Content =
                    new StringContent(
                        cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message),
                        Encoding.UTF8, "application/json");

                return httpResponseMessage;
            }
        }

        private static string GetFilePath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }

        private string DataTableToCsv(DataTable dataTable, string fileFullNameWithPath)
        {
            var sb = new StringBuilder();

            foreach (DataRow row in dataTable.Rows)
            {
                var fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join("\t", fields));
            }

            try
            {
                File.WriteAllText(fileFullNameWithPath, sb.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return fileFullNameWithPath;
        }

        private static ICollection<CustomerStatementMessage> GetParsedTransactionFile(IMt940Format format,
            string resourceName)
        {
            return GetParsedTransactionFile(format, resourceName, CultureInfo.GetCultureInfo("nl-NL"));
        }

        private static ICollection<CustomerStatementMessage> GetParsedTransactionFile(IMt940Format format,
            string resourceName, CultureInfo cultureInfo)
        {
            using (var reader = new StreamReader(resourceName))
            {
                return Mt940Parser.Parse(format, reader, cultureInfo);
            }
        }
    }
}