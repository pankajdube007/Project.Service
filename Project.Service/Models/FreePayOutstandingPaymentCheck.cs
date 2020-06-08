using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofFreePayOutstandingPayment
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
        public string savedamounttotal { get; set; }

        [Required]
        public string withdiscountamounttotal { get; set; }

   
        public string devicetype { get; set; }


        public string deviceid { get; set; }
    }

    public class FreePayOutstandingPayments
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FreePayOutstandingPayment> data { get; set; }
    }

    public class FreePayOutstandingPayment
    {
        public string transno { get; set; }
        public string Receipt { get; set; }
        public string grandtotal { get; set; }
        public string savedamounttotal { get; set; }
        public string withdiscountamounttotal { get; set; }
        public List<FreePayOutstandingInvoice> invoices { get; set; }
    }

    public class FreePayOutstandingInvoice
    {
        public string InvoiceId { get; set; }
        public string InvoiceAmount { get; set; }
        public string CatId { get; set; }
        public string DiscountedAmount { get; set; }
        public string EnteredAmount { get; set; }
        public string SavedAmount { get; set; }
        public string OutstandingAmount { get; set; }
        public string Per { get; set; }
    }
}