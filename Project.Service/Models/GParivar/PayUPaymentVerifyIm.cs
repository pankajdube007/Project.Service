using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.PayU
{
    public class PayUPaymentVerifyIm
    {
        [Required] public string CIN { get; set; }

        [Required] public string ClientSecret { get; set; }

        [Required] public string txnid { get; set; }
    }

    public class TransactionDetails
    {
        public string mihpayid { get; set; }
        public string request_id { get; set; }
        public string bank_ref_num { get; set; }
        public string amt { get; set; }
        public string transaction_amount { get; set; }
        public string txnid { get; set; }
        public string additional_charges { get; set; }
        public string productinfo { get; set; }
        public string firstname { get; set; }
        public string bankcode { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string field2 { get; set; }
        public string field9 { get; set; }
        public string error_code { get; set; }
        public string addedon { get; set; }
        public string payment_source { get; set; }
        public string card_type { get; set; }
        public string error_Message { get; set; }
        public string net_amount_debit { get; set; }
        public string disc { get; set; }
        public string mode { get; set; }
        public string PG_TYPE { get; set; }
        public string card_no { get; set; }
        public string status { get; set; }
        public string unmappedstatus { get; set; }
        public string Merchant_UTR { get; set; }
        public string Settled_At { get; set; }
        public string App_Name { get; set; }
    }


    public class PayUPaymentVerify
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TransactionDetails> data { get; set; }
    }
}