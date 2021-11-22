using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{
   
    public class ListofVendorPurchaseInvoicereport
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Todate { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string PartyID { get; set; }

      

        [Required]
        public string Cat { get; set; }

    }

    public class VendorPurchaseInvoicereports
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorPurchaseInvoicereport> data { get; set; }
    }

    public class VendorPurchaseInvoicereport
    {
        public string uniquekey { get; set; }
        public string SLNo { get; set; }
        public string invoiceno { get; set; }
        public string invoicedate { get; set; }
        public string itemamount { get; set; }
        public string discountamount { get; set; }
        public string taxamount { get; set; }
        public string totalamount { get; set; }
        public string finalamount { get; set; }
        public string roundoff { get; set; }
        public string EWayBillno { get; set; }
        public string divisionnm { get; set; }
        public string BranchName { get; set; }
        public string taxamount2 { get; set; }
        public string url { get; set; }
        public string Party { get; set; }


    }
}