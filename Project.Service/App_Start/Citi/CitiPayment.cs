using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Project.Service.Citi
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Project.Service.Citi.ICitiPayment" />
    public class CitiPayment : ICitiPayment
    {
        private const string GeplBenResponseFilename = "BENIGOLDMEDAL";
        private const string GeplTransactionResponseFilename = "MT940_H2H_";
        private const string LocalBaseDirectory = "~/App_Data/citi/download";
        private readonly string _downloadBaseDirectory;

        private readonly ISftpRemoteClient _remoteClient;
        private readonly string _uploadBaseDirectory;

        /// <summary>
        ///     Ctor
        /// </summary>
        public CitiPayment()
        {
            var host = ConfigurationManager.AppSettings["Citi.Host"];
            var port = ConfigurationManager.AppSettings["Citi.Port"];
            var username = ConfigurationManager.AppSettings["Citi.UserName"];
            var password = ConfigurationManager.AppSettings["Citi.Password"];
            var keyFilePath =
                GetActualFilePath("~/App_Data/citi/" +
                                  ConfigurationManager.AppSettings["Citi.PrivateKeyFile"]);

            var uploadBaseDirectory = ConfigurationManager.AppSettings["Citi.Root.UploadDirectory"];
            var downloadBaseDirectory = ConfigurationManager.AppSettings["Citi.Root.Directory"];

            _uploadBaseDirectory = uploadBaseDirectory;
            _downloadBaseDirectory = downloadBaseDirectory;
            _remoteClient = new SftpRemoteClient(host, port, username, password, keyFilePath);
        }

        /// <summary>
        ///     Uploads the beneficiary details.
        /// </summary>
        /// <param name="localFilePath">The local file path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public void UploadBeneficiaryDetails(string localFilePath, string fileName)
        {
            var remoteFilePath = $"{_uploadBaseDirectory}/{fileName}";

            _remoteClient.UploadFileToRemoteServer(localFilePath, remoteFilePath);
        }

        /// <summary>
        ///     Uploads the payment transaction.
        /// </summary>
        /// <param name="localFilePath">The local file path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public void UploadPaymentTransaction(string localFilePath, string fileName)
        {
            var remoteFilePath = $"{_uploadBaseDirectory}/{fileName}";

            _remoteClient.UploadFileToRemoteServer(localFilePath, remoteFilePath);
        }

        /// <summary>
        ///     Downloads the citi bank transaction response file.
        /// </summary>
        /// <returns></returns>
        public string DownloadCitiBankTransactionResponseFile()
        {
            var listOfFilesInDirectory = ListFilesInDownloadDirectory();
            if (!listOfFilesInDirectory.Any()) return "";

            var transactionFileList =
                listOfFilesInDirectory.Where(f => f.Contains(GeplTransactionResponseFilename))?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(transactionFileList)) return "";

            var localDirectory = GetActualFilePath($"{LocalBaseDirectory}/transaction");
            _remoteClient.DownloadRemoteFile($"{_downloadBaseDirectory}{transactionFileList}", localDirectory);

            return transactionFileList;
        }

        /// <summary>
        ///     Downloads the citi bank beneficiary response file.
        /// </summary>
        /// <returns></returns>
        public string DownloadCitiBankBeneficiaryResponseFile()
        {
            var listOfFilesInDirectory = ListFilesInDownloadDirectory();
            if (!listOfFilesInDirectory.Any()) return "";

            var transactionFileList =
                listOfFilesInDirectory.Where(f => f.Contains(GeplBenResponseFilename))?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(transactionFileList)) return "";

            var localBaseBeneficiaryDirectory = GetActualFilePath($"{LocalBaseDirectory}/beneficiary");
            _remoteClient.DownloadRemoteFile($"{_downloadBaseDirectory}{transactionFileList}",
                localBaseBeneficiaryDirectory);

            return transactionFileList;
        }

        /// <summary>
        ///     Lists the files in download directory.
        /// </summary>
        /// <returns></returns>
        private List<string> ListFilesInDownloadDirectory()
        {
            return _remoteClient.ListFilesFromServer(_downloadBaseDirectory);
        }

        /// <summary>
        ///     Gets the actual file path.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        private static string GetActualFilePath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }
    }
}