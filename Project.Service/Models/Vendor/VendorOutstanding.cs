using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListsofVendorOutstanding
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Category { get; set; }

    }


    public class VendorOutstandings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorOutstandingFinal> data { get; set; }
    }

    public class VendorOutstandingFinal
    {
        public List<VendorOutstanding> details { get; set; }
        public List<VendorOutstandingTotal> Total { get; set; }
    }

    public class VendorOutstanding
    {
        public int InvoiceId { get; set; }
        public int CatId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string DivisionName { get; set; }
        public string InvoiceAmt { get; set; }
        public string Payamt { get; set; }
        public string DueDays { get; set; }
        public string percent { get; set; }
        public string interestamt { get; set; }
    }
    public class VendorOutstandingTotal
    {
        public string totalinvoiceamt { get; set; }
        public string totalpayamt { get; set; }
    }

    }