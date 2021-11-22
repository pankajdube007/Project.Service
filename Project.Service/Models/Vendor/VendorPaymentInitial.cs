using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofVendorPaymentInitial
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public object OrderDetails { get; set; }

        [Required]
        public string grandtotal { get; set; }

        [Required]
        public string interestamounttotal { get; set; }

        [Required]
        public string paytotal { get; set; }

        [Required]
        public string devicetype { get; set; }

        [Required]
        public string deviceid { get; set; }
    }

    public class VendorPaymentInitials
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorPaymentInitial> data { get; set; }
    }

    public class VendorPaymentInitial
    {
        public string transno { get; set; }
        public string Receipt { get; set; }
        public string grandtotal { get; set; }
        public string interestamounttotal { get; set; }
        public string paytotal { get; set; }
        public List<VendorPaymentInitialInvoice> invoices { get; set; }
    }

    public class VendorPaymentInitialInvoice
    {
        public string InvoiceId { get; set; }
        public string CatId { get; set; }
        public string InvoiceAmount { get; set; }
        public string PayAmount { get; set; }
        public string EnteredAmount { get; set; }
        public string InterestAmount { get; set; }
        public string OutstandingAmount { get; set; }
        public string Per { get; set; }
    }
}