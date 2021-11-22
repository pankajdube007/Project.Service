using System;
using System.Collections.Generic;

namespace Project.Service.Models.Citi
{
    public class GoldCitiBank
    {
        public string AccountNo { get; set; }
        public string OpeningBalance { get; set; }
        public string ClosingBalance { get; set; }
        public DateTime? StatementDate { get; set; }
        public List<CitiPayoutTransaction> CitiPayoutTransactions { get; set; }
    }
}