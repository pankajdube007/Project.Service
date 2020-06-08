using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofFreepayPaymentRequest
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public string transactionid { get; set; }
        [Required] public string payBankCode { get; set; }
    }


    public class PaymentInfo
    {
        public string payId { get; set; }
        public double payAmount { get; set; }
        public string payBankCode { get; set; }
        public string payDate { get; set; }
    }

    public class CreditNote
    {
        //public string cnNumber { get; set; }
        //public string cnDueDate { get; set; }
        //public string cnOriginalAmount { get; set; }
        //public string cnUtilizationAmount { get; set; }
    }

    public class Invoice1
    {
        public string paydocNumber { get; set; }
        public string paydocType { get; set; }
        public string paydocDueDate { get; set; }
        public double paydocOriginalAmount { get; set; }
        public double paydocOutstandingAmount { get; set; }
        public double paydocCdApplied { get; set; }
        public double paydocPayAmount { get; set; }
        public List<CreditNote> creditNotes { get; set; }
    }

    public class RootObject
    {
        public PaymentInfo paymentInfo { get; set; }
        public List<Invoice1> invoices { get; set; }
    }

    public class FreepayPaymentRequest
    {
        public string mobile { get; set; }
        public string email { get; set; }
    }

    public class FreepayPaymentRequests
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FreepayPaymentRequest> data { get; set; }
    }

}