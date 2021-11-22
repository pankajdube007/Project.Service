namespace Project.Service.Citi
{
    public interface ICitiPayment
    {
        /// <summary>
        ///     Uploads the beneficiary details.
        /// </summary>
        /// <param name="localFilePath">The local file path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        void UploadBeneficiaryDetails(string localFilePath, string fileName);

        /// <summary>
        ///     Uploads the payment transaction.
        /// </summary>
        /// <param name="localFilePath">The local file path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        void UploadPaymentTransaction(string localFilePath, string fileName);

        /// <summary>
        ///     Downloads the citi bank transaction response file.
        /// </summary>
        /// <returns></returns>
        string DownloadCitiBankTransactionResponseFile();

        /// <summary>
        ///     Downloads the citi bank beneficiary response file.
        /// </summary>
        /// <returns></returns>
        string DownloadCitiBankBeneficiaryResponseFile();
    }
}