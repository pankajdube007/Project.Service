using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.PayU
{
    public class SattlementDetailsIm
    {
        [Required] public string CIN { get; set; }

        [Required] public string ClientSecret { get; set; }

        public DateTime? Date { get; set; }
    }

    public class SattlementTxnDetail
    {
        public string payuid { get; set; }
        public string txnid { get; set; }
        public string txndate { get; set; }
        public string mode { get; set; }
        public string amount { get; set; }
        public string requestid { get; set; }
        public string requestdate { get; set; }
        public string requestaction { get; set; }
        public string requestamount { get; set; }
        public string mer_utr { get; set; }
        public string mer_service_fee { get; set; }
        public string mer_service_tax { get; set; }
        public string mer_net_amount { get; set; }
        public string bank_name { get; set; }
        public object issuing_bank { get; set; }
        public string merchant_subvention_amount { get; set; }
        public string cgst { get; set; }
        public string igst { get; set; }
        public string sgst { get; set; }
        public string PG_TYPE { get; set; }
        public object CardType { get; set; }
    }

    public class SattlementDetailsVm
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<SattlementTxnDetail> Txn_details { get; set; }
    }
}