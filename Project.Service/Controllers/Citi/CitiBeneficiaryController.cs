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
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using NLog;
using Project.Service.Citi;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.Citi;

namespace Project.Service.Controllers.Citi
{
    public class CitiBeneficiaryController : ApiController
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private const string LocalBaseDirectory = "~/App_Data/citi/upload";
        private const string GeplBeneficiaryFilename = "gepl-beneficiary";
        private const string FILE_DIRECTORY_NAME = "citi/upload";
        private const string LocalBaseDownloadDirectory = "~/App_Data/citi/download/beneficiary";

        [HttpPost]
        [ValidateModel]
        [Route("api/citi/make-ben-payload")]
        public HttpResponseMessage CitiBenPayload()
        {
            var cm = new Common();
            try
            {
                var g1 = new DataConnectionTrans();

                //Step-1 get all data
                var getAllPendingBen = g1.return_dt("exec getALLPendingBen");

                if (getAllPendingBen.Rows.Count > 0)
                {
                    var fileName =
                        $"{GeplBeneficiaryFilename}-{DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss_tt").ToLower()}.txt";
                    var fileFullNameWithPath = $"{LocalBaseDirectory}/beneficiary/{fileName}";
                    var getFilePath = GetFilePath(fileFullNameWithPath);

                    //Save to local
                    var filePath = DataTableToCsv(getAllPendingBen, getFilePath);

                    if (File.Exists(filePath))
                    {
                        //Step-2:Save to blob
                        var _goldMedia = new GoldMedia();
                        Dictionary<bool, string> retStr;

                        using (Stream fileStream = new FileStream(getFilePath, FileMode.Open))
                        {
                            retStr = _goldMedia.GoldMediaUpload(fileName, $"{FILE_DIRECTORY_NAME}/beneficiary", "",
                                fileStream,
                                "text/plain", false);
                        }

                        if (retStr.Keys.FirstOrDefault())
                        {
                            var blobfilename = _goldMedia.MapPathToPublicUrl(retStr.Values.FirstOrDefault());

                            //Step-3
                            g1.ExecDB(
                                $"exec CitiFtpBenJobsAddStep1 '{fileFullNameWithPath}','{blobfilename}','{fileName}'");
                            var response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(JsonConvert.SerializeObject(getAllPendingBen),
                                Encoding.UTF8, "application/json");

                            return response;
                        }
                        else
                        {
                            g1.close_connection();
                            var response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Unable to upload file to blob"),
                                Encoding.UTF8, "application/json");

                            return response;
                        }
                    }

                    {
                        g1.close_connection();
                        var response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content =
                            new StringContent(cm.StatusTime(false, "Unable to generate file in local storage"),
                                Encoding.UTF8, "application/json");

                        return response;
                    }
                }

                {
                    g1.close_connection();
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8,
                        "application/json");

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

        [HttpPost]
        [ValidateModel]
        [Route("api/citi/ben-add")]
        public HttpResponseMessage CitiBenSync()
        {
            var cm = new Common();
            try
            {
                var g1 = new DataConnectionTrans();
                ICitiPayment remoteClient = new CitiPayment();

                var getAllPendingBen = g1.return_dt("exec getaAllBenFileTobeUpload");

                if (getAllPendingBen.Rows.Count > 0)
                {
                    var _goldMedia = new GoldMedia();


                    var fileName = getAllPendingBen.Rows[0]["FileName"].ToString();
                    var remoteFileNameAndPath =
                        $"{FILE_DIRECTORY_NAME}/beneficiary/{fileName}"; // getAllPendingPayment.Rows[0]["BlobFileLink"].ToString();
                    var fileFullNameWithPath = $"{LocalBaseDirectory}/beneficiary/temp/{fileName}";


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
                        remoteClient.UploadBeneficiaryDetails(GetFilePath(fileFullNameWithPath), fileName);


                        //Step-4:Update Status and fileName
                        var result = g1.ExecDB("exec CitiFtpBenJobsAddStep2 " + getAllPendingBen.Rows[0]["Id"]);
                        var response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(JsonConvert.SerializeObject(getAllPendingBen),
                            Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g1.close_connection();
                        var response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8,
                            "application/json");

                        return response;
                    }
                }

                {
                    g1.close_connection();
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8,
                        "application/json");

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

        [HttpPost]
        [ValidateModel]
        [Route("api/citi/ben/status")]
        public HttpResponseMessage CitiBenStatusSync()
        {
            var cm = new Common();
            try
            {
                var row = 0;
                var actualFileName = string.Empty;
                var g1 = new DataConnectionTrans();
                ICitiPayment remoteClient = new CitiPayment();

                var listBenFiles = remoteClient.DownloadCitiBankBeneficiaryResponseFile();
                //var listBenFiles = "BENIGOLDMEDAL202109191633";


                if (!string.IsNullOrWhiteSpace(listBenFiles))
                {
                    actualFileName = $"{listBenFiles}.txt";
                    List<CitiBenResponse> records;
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true,
                        BadDataFound = null
                    };

                    var filePath = GetFilePath($"{LocalBaseDownloadDirectory}/{actualFileName}");

                    using (TextReader sr = new StreamReader(filePath, Encoding.Unicode))
                    {
                        using (var csvReader = new CsvReader(sr, config))
                        {
                            csvReader.Context.RegisterClassMap<CitiBenResponseMap>();
                            records = csvReader.GetRecords<CitiBenResponse>().ToList();
                        }
                    }
                    //using (var dr = new CsvDataReader(csv))
                    //{
                    //    var dt = new DataTable();
                    //    dt.Columns.Add("Id", typeof(int));
                    //    dt.Columns.Add("Name", typeof(string));

                    //    dt.Load(dr);
                    //}

                    //var recordsToUpdate = records.Where(r => r.Status.Trim().ToLower() == "a proccessed recoreds" && r.LastUsedDate > DateTime.Now.AddDays(-4)).ToList();
                    var recordsToUpdate = records.Where(r =>
                        Convert.ToDateTime(string.IsNullOrWhiteSpace(r.LastModifiedDateUserName)
                            ? "2020-04-31"
                            : r.LastModifiedDateUserName).Date > DateTime.Now.AddDays(-60).Date).ToList();
                    Logger.Info("modifed date conversion succeded in step 1");



                    foreach (var ben in recordsToUpdate)
                    {
                        var lastModifiedDateUserName = Convert.ToDateTime(string.IsNullOrWhiteSpace(ben.LastModifiedDateUserName)
                            ? "2020-04-31"
                            : ben.LastModifiedDateUserName).Date;
                        Logger.Info("modifed date conversion succeded in step 2");
                        row = g1.ExecDB(
                            $"exec benStatusUpdate '{ben.PreformatCode}','{ben.BeneficiaryAccountNumber}','{ben.Status}','{lastModifiedDateUserName:yyyy-MM-dd}'");
                        if (row > 0) row++;
                    }

                    //Step-2:Save to blob
                    var _goldMedia = new GoldMedia();
                    Dictionary<bool, string> retStr;
                    var getFilePath = GetFilePath($"{LocalBaseDownloadDirectory}/{actualFileName}");
                    using (Stream fileStream = new FileStream(getFilePath, FileMode.Open))
                    {
                        retStr = _goldMedia.GoldMediaUpload(actualFileName, $"{FILE_DIRECTORY_NAME}/beneficiary/status",
                            "",
                            fileStream,
                            "text/plain", false);
                    }

                    if (retStr.Keys.FirstOrDefault())
                    {
                        var blobfilename = _goldMedia.MapPathToPublicUrl(retStr.Values.FirstOrDefault());

                        g1.ExecDB(
                            $"exec citiStatusFileNameUpdate '{blobfilename}','{actualFileName}'");
                    }

                    var statement = JsonConvert.SerializeObject(recordsToUpdate);

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content =
                        new StringContent(statement,
                            Encoding.UTF8, "application/json");

                    return response;
                }

                var response3 = Request.CreateResponse(HttpStatusCode.OK);
                response3.Content =
                    new StringContent(cm.StatusTime(false, "No records in file available"),
                        Encoding.UTF8, "application/json");

                return response3;
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

        private static string GetFilePath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }

        private string DataTableToCsv(DataTable dataTable, string fileFullNameWithPath)
        {
            var sb = new StringBuilder();

            var columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join("\t", columnNames));

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
                Logger.Error(e,$"An error has been raised when converting csv to data table with msg {e.Message}");
                throw;
            }

            return fileFullNameWithPath;
        }
    }
}