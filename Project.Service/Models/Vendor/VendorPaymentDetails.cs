using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   

    public class GetVendorPaymentDetailsList
    {
        [Required] public string CIN { get; set; }
        [Required] public string Category { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public string fromdate { get; set; }
        [Required] public string todate { get; set; }
    }

    public class VendorPaymentDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorPaymentDetails> data { get; set; }
    }

    public class VendorPaymentDetails
    {
        public int slno { get; set; }
        public string Receipt { get; set; }
        public string Voucherdt { get; set; }
        public decimal payamt { get; set; }
        public string status { get; set; }
        public int statusflag { get; set; }
        public decimal totalamt { get; set; }
        public decimal intamt { get; set; }
        public string transactionid { get; set; }
        public bool retry { get; set; }
        public string paymentmode { get; set; }
        public string paymenttype { get; set; }

    }
}