using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.PayU
{
    public class PayUPaymentVerificationByDatesIm
    {
        [Required] public string CIN { get; set; }

        [Required] public string ClientSecret { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class TransactionDetail
    {
        public string id { get; set; }
        public string action { get; set; }
        public string status { get; set; }
        public string issuing_bank { get; set; }
        public string transaction_fee { get; set; }
        public string key { get; set; }
        public string merchantname { get; set; }
        public string txnid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string addedon { get; set; }
        public string bank_name { get; set; }
        public string payment_gateway { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string amount { get; set; }
        public string discount { get; set; }
        public string additional_charges { get; set; }
        public string productinfo { get; set; }
        public string error_code { get; set; }
        public string bank_ref_no { get; set; }
        public string ibibo_code { get; set; }
        public string mode { get; set; }
        public string ip { get; set; }
        public string card_no { get; set; }
        public string cardtype { get; set; }
        public string offer_key { get; set; }
        public string field2 { get; set; }
        public string udf1 { get; set; }
        public string pg_mid { get; set; }
        public string offer_type { get; set; }
        public string failure_reason { get; set; }
        public string mer_service_fee { get; set; }
        public string mer_service_tax { get; set; }
    }

    public class PayUPaymentVerificationByDates
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<TransactionDetail> Transaction_details { get; set; }
    }
}