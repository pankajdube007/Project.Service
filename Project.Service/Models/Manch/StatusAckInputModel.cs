using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.Manch.Status
{
    public class StatusAckInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid CIN")]
        public string CIN { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid From date")]
        public string RequestId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid To date")]
        public string StatusCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide a valid To date")]
        public string StatusMessage { get; set; }
    }

    public class SignerInfo
    {
        public string commonName { get; set; }
        public string title { get; set; }
        public string yob { get; set; }
        public string gender { get; set; }
    }

    public class EsignAttempt
    {
        public string attemptId { get; set; }
        public string esignStatus { get; set; }
        public string responseCode { get; set; }
        public string message { get; set; }
    }

    public class Document
    {
        public string documentType { get; set; }
        public string documentStorageId { get; set; }
        public string documentURL { get; set; }
        public List<SignerInfo> signerInfo { get; set; }
        public string signed { get; set; }
        public List<EsignAttempt> esignAttempts { get; set; }
    }

    public class Data
    {
        public string transactionState { get; set; }
        public List<Document> documents { get; set; }
    }

    public class TransactionStatus
    {
        public string requestId { get; set; }
        public string responseCode { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
