using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofOutstandingReportAction
    {
        [Required]
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

        public string PayType { get; set; } = "qwikpay";
    }

    public class OutstandingReports
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OutstandingFinal> data { get; set; }
    }

    public class OutstandingFinal
    {
        public List<OutstandingReport> outstandingdata { get; set; }
        public List<TotalOutstanding> totaloutstanding { get; set; }
        public List<TotalDue> Totaldueoverdue { get; set; }
        public bool ismore { get; set; }
    }

    public class TotalOutstanding
    {
        public string InvoiceAmt { get; set; }
        public string OuststandingAmt { get; set; }
    }

    public class TotalDue
    {
        public string Due { get; set; }
        public string OverDue { get; set; }
    }

    public class OutstandingReport
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string DivisionName { get; set; }
        public string InvoiceAmt { get; set; }
        public string OuststandingAmt { get; set; }
        public string DueDays { get; set; }
        public string cddate { get; set; }
        public string percent { get; set; }
        public string duestatus { get; set; }
        public decimal extraper { get; set; }
    }

    public class OutstandingReportDue
    {
        public string Due { get; set; }
        public string OverDue { get; set; }
    }
}