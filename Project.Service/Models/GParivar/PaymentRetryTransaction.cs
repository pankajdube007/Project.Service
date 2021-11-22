using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    
    public class ListofPaymentRetryTransaction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string transactionid { get; set; }
    }

    public class PaymentRetryTransactions
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PaymentRetryTransaction> data { get; set; }
    }

    public class PaymentRetryTransaction
    {
        public string transno { get; set; }
        public string Receipt { get; set; }
        public string grandtotal { get; set; }
        public string savedamounttotal { get; set; }
        public string withdiscountamounttotal { get; set; }
        public List<PaymentRetryTransactionInvoice> invoices { get; set; }
    }

    public class PaymentRetryTransactionInvoice
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