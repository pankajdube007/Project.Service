using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.Manch
{
    public class SignDocumentInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid CIN")]
        public string CIN { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid From date")]
        public string FromDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid To date")]
        public string ToDate { get; set; }
    }

    public class Document
    {
        public string documentType { get; set; }
        //public string documentBytes { get; set; }
        public string documentTypeUrl { get; set; }
        //public string documentStorageId { get; set; }
    }

    public class ManchPayload
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string templateKey { get; set; }
        public string esignMethod { get; set; } = "OTP";
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public string returnURL { get; set; }
        public List<Document> documents { get; set; }
    }

    //Return
    public class Transaction
    {
        public string transactionId { get; set; }
        public string transactionLink { get; set; }
    }

    public class RetDocument
    {
        public string documentType { get; set; }
        public string documentId { get; set; }
        public string documentLink { get; set; }
    }

    public class Data
    {
        public Transaction transaction { get; set; }
        public List<RetDocument> documents { get; set; }
    }

    public class ManchTransactionResponse
    {
        public string requestId { get; set; }
        public string responseCode { get; set; }
        public Data data { get; set; }
    }

    public class ManchResponseToClientApp
    {
        public string hashToken { get; set; }
        public string requestId { get; set; }
        public string documentLink { get; set; }
    }
    public class ManchResponseToClientApps
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ManchResponseToClientApp> data { get; set; }
    }



    // Esign ledger

    public class SignLedgerInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid CIN")]
        public string CIN { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid From date")]
        public string FromDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid To date")]
        public string ToDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid ClientSecret")]
        public string ClientSecret { get; set; }
    }


    public class SignLedgerOutputModelLINK
    {
        
        public string link { get; set; }
        public string amount { get; set; }
        public bool IsExists { get; set; }
    }


    public class SignLedgerOutputModel
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SignLedgerOutputModelLINK> data { get; set; }
    }


    //esign ledger report


    public class SignLedgerReportInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid CIN")]
        public string CIN { get; set; }

   
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid ClientSecret")]
        public string ClientSecret { get; set; }
    }

    public class SignLedgerReportOutputModel
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SignLedgerReportData> data { get; set; }
    }

    public class SignLedgerReportData
    {
        public List<SignLedgerReport> signdata { get; set; }
        public List<SignLedgerstatus> status { get; set; }
    }


        public class SignLedgerReport
    {
        public string year { get; set; }
        public string quater { get; set; }
        public string amount { get; set; }
        public string link { get; set; }

    }

    public class SignLedgerstatus
    {
        public bool Isactive { get; set; }

    }
}
