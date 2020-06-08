using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListsofFreepayOutstandingReport
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
    }

    public class FreepayOutstandingReports
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FreepayOutstandingReportFinal> data { get; set; }
    }

    public class FreepayOutstandingReportFinal
    {
        public List<FreepayOutstandingReport> outstandingdata { get; set; }
        public List<FreepayTotalOutstanding> totaloutstanding { get; set; }
        public List<FreepayTotalDue> Totaldueoverdue { get; set; }
        public bool ismore { get; set; }
        public bool isregistered { get; set; }
    }

    public class FreepayTotalOutstanding
    {
        public string InvoiceAmt { get; set; }
        public string OuststandingAmt { get; set; }
    }

    public class FreepayTotalDue
    {
        public string Due { get; set; }
        public string OverDue { get; set; }
    }

    public class FreepayOutstandingReport
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string DivisionName { get; set; }
        public string CatId { get; set; }
        public string InvoiceAmt { get; set; }
        public string OuststandingAmt { get; set; }
        public string DueDays { get; set; }
        public string cddate { get; set; }
        public string percent { get; set; }
        public string duestatus { get; set; }
        public decimal extraper { get; set; }
    }

    public class FreepayOutstandingReportDue
    {
        public string Due { get; set; }
        public string OverDue { get; set; }
    }
}