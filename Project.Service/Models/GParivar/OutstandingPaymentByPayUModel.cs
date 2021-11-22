using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.PayU
{
    public class OutstandingPaymentByPayUModel
    {
        [Required] public string CIN { get; set; }

        [Required] public string Category { get; set; }

        [Required] public string ClientSecret { get; set; }

        [Required] public object OrderDetails { get; set; }

        [Required] public string grandtotal { get; set; }

        [Required] public string savedamounttotal { get; set; }

        [Required] public string withdiscountamounttotal { get; set; }


        public string devicetype { get; set; }


        public string deviceid { get; set; }
    }

    public class PayUPayments
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PayUPayment> data { get; set; }
    }

    public class PayUPayment
    {
        public string DiscTotal { get; set; } = "0.0";
        public string PaymentAmt { get; set; } = "0.0";
        public List<PayUOutstandingInvoice> Invoices { get; set; }
        public List<PayUPayload> Payload { get; set; }

        public string GrandTotal =>
            (Convert.ToDecimal(DiscTotal) + Convert.ToDecimal(PaymentAmt)).ToString();
    }

    public class PayUOutstandingInvoice
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

    public class PayUPayload
    {
        public string txnid { get; set; }
        public string amount { get; set; }
        public string productinfo { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        //public string lastname { get; set; }
        public string surl { get; set; }
        public string furl { get; set; }
        public string curl { get; set; }
        public string hash { get; set; }
    }
}