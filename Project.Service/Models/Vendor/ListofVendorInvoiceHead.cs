using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofVendorInvoiceHead
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string datefrom { get; set; }

        [Required]
        public string dateto { get; set; }

        [Required]
        public int PartyID { get; set; }

        [Required]
        public int TypeCat { get; set; }

        [Required]
        public string Cat { get; set; }

    }

    public class GetVendorInvoiceHeadDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetVendorInvoiceHeadDetail> data { get; set; }
    }

    public class GetVendorInvoiceHeadDetail
    {
        public string SLNo { get; set; }
        public string invoiceno { get; set; }
        public string invoicedate { get; set; }
        public double itemamount { get; set; }
        public double taxamount { get; set; }
        public double totalamount { get; set; }
        public double finalamount { get; set; }
        public double roundoff { get; set; }
        public string EWayBillno { get; set; }
        public string BranchName { get; set; }
        public double taxamount2 { get; set; }



    }
}