using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  


    public class ListPurchaseAndLedgerBalancePaymentSupplier
    {
        [Required]
        public int SupplierId { get; set; }

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

    public class PurchaseAndLedgerBalancePaymentSuppliers
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<PurchaseAndLedgerBalancePaymentSupplierFinal> data { get; set; }
    }

    public class PurchaseAndLedgerBalancePaymentSupplierFinal
    {
        public List<PurchaseAndLedgerBalancePaymentSupplier> PurchaseAndLedgerBalancePaymentSupplierdata { get; set; }
        public bool ismore { get; set; }
    }

    public class PurchaseAndLedgerBalancePaymentSupplier
    {

        public int slno { get; set; }
     
        public string VoucherNo { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
    }


}