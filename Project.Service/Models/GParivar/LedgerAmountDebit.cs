using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofLedgerAmountDebitAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string FinYear { get; set; }

        public string searchtxt { get; set; }
        //[Required]
        public int ReportType { get; set; }

        //[Required]
        public int ReportValue { get; set; }
    }

    public class LedgerAmountDebits
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LedgerAmountDebit> data { get; set; }
    }

    public class LedgerAmountDebit
    {
        public string referenceno { get; set; }
        public string date { get; set; }
        public string amount { get; set; }
        public string ledgerdec { get; set; }
        public string url { get; set; }
    }
}