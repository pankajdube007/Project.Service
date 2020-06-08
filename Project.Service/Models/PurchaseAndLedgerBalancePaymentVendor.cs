using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
   



    public class ListPurchaseAndLedgerBalancePaymentVendor
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

    public class PurchaseAndLedgerBalancePaymentVendors
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<PurchaseAndLedgerBalancePaymentVendorFinal> data { get; set; }
    }

    public class PurchaseAndLedgerBalancePaymentVendorFinal
    {
        public List<PurchaseAndLedgerBalancePaymentVendor> PurchaseAndLedgerBalancePaymentVendordata { get; set; }
        public bool ismore { get; set; }
    }

    public class PurchaseAndLedgerBalancePaymentVendor
    {

        public int slno { get; set; }
     
        public string VoucherNo { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
    }
}