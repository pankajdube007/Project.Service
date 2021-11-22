using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.PayU
{
    public class PayUResponseIm
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string PayuResponse { get; set; }
    }
    public class PayUResponseDto
    {
        public string id { get; set; }
        public string mode { get; set; }
        public string status { get; set; }
        public string unmappedstatus { get; set; }
        public string key { get; set; }
        public string txnid { get; set; }
        public string transaction_fee { get; set; }
        public string amount { get; set; }
        public string discount { get; set; }
        public string addedon { get; set; }
        public string productinfo { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string hash { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
        public string field6 { get; set; }
        public string field7 { get; set; }
        public string field8 { get; set; }
        public string field9 { get; set; }
        public string field10 { get; set; }
        public string payment_source { get; set; }
        public string PG_TYPE { get; set; }
        public string bank_ref_no { get; set; }
        public string ibibo_code { get; set; }
        public string error_code { get; set; }
        public string Error_Message { get; set; }
        public string is_seamless { get; set; }

    }
}