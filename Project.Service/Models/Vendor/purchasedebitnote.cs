using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{
    public class ListsofPurchaseDebitNoteAction
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

    public class PurchaseDebitNotes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PurchaseDebitNote> data { get; set; }
    }

    public class PurchaseDebitNote
    {
        public int slno { get; set; }
        public string referenceno { get; set; }
        public string date { get; set; }
        public string amount { get; set; }
        public string ledgerdec { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string download { get; set; }
    }
}