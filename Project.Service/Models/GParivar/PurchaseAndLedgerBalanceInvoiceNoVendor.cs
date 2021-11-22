using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
   
    public class ListPurchaseAndLedgerBalanceInvoiceNoVendor
    {
        [Required]
        public int VendorId { get; set; }

        [Required]
        public string Cat { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int Count { get; set; }
    }

    public class PurchaseAndLedgerBalanceInvoiceNoVendors
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<PurchaseAndLedgerBalanceInvoiceNoVendorFinal> data { get; set; }
    }

    public class PurchaseAndLedgerBalanceInvoiceNoVendorFinal
    {
        public List<PurchaseAndLedgerBalanceInvoiceNoVendor> PurchaseAndLedgerBalanceInvoiceNoVendordata { get; set; }
        public bool ismore { get; set; }
    }

    public class PurchaseAndLedgerBalanceInvoiceNoVendor
    {
     
        public int slno { get; set; }
        public string UniqueKey { get; set; }
        public string InvoiceNo { get; set; }
        public string Date { get; set; }
        public string Fileurl { get; set; }
        public decimal Amount { get; set; }
    }


}