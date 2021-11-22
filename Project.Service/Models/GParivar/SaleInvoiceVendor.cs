using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{

    public class ListsaleAndLedgerBalanceInvoiceNoVendor
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

    public class saleAndLedgerBalanceInvoiceNoVendors
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<saleAndLedgerBalanceInvoiceNoVendorFinal> data { get; set; }
    }

    public class saleAndLedgerBalanceInvoiceNoVendorFinal
    {
        public List<saleAndLedgerBalanceInvoiceNoVendor> saleAndLedgerBalanceInvoiceNoVendordata { get; set; }
        public bool ismore { get; set; }
    }

    public class saleAndLedgerBalanceInvoiceNoVendor
    {

        public int slno { get; set; }
        public string InvoiceNo { get; set; }
        public string Date { get; set; }
        public string Fileurl { get; set; }
        public decimal Amount { get; set; }
    }
}