using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofDivisionSalesReportAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FinYear { get; set; }

        [Required]
        public int ReportType { get; set; }

        [Required]
        public int ReportValue { get; set; }
    }

    public class DivisionSalesReports
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DivisionSalesReportall> data { get; set; }
    }

    public class DivisionSalesReport
    {
        public string Division { get; set; }
        public string AmountinLk { get; set; }
        public string Amount { get; set; }
    }

    public class DivisionSalesReportall
    {
        public List<DivisionSalesReport> DivisionSalesReport { get; set; }
        public string TotalAmtinlk { get; set; }
        public string TotalAmt { get; set; }
    }
}