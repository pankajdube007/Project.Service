using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofOutstandingReportExAction
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Division { get; set; }

        [Required]
        public int OutstangingDays { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class OutstandingReportExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OutstandingFinalEx> data { get; set; }
    }

    public class OutstandingFinalEx
    {
        public List<OutstandingReportEx> outstandingdata { get; set; }
        public List<TotalOutstandingEx> totaloutstanding { get; set; }
        public List<TotalDueEx> Totaldueoverdue { get; set; }
        public bool ismore { get; set; }
    }

    public class TotalOutstandingEx
    {
        public string InvoiceAmt { get; set; }
        public string OuststandingAmt { get; set; }
    }

    public class TotalDueEx
    {
        public string Due { get; set; }
        public string OverDue { get; set; }
    }

    public class OutstandingReportEx
    {
        public string PartyName { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string DivisionName { get; set; }
        public string InvoiceAmt { get; set; }
        public string OuststandingAmt { get; set; }
        public string DueDays { get; set; }
    }

    public class OutstandingReportDueEx
    {
        public string Due { get; set; }
        public string OverDue { get; set; }
    }
}