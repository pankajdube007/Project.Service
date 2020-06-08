using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class DivisionSalesReportExAction
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FinYear { get; set; }

        [Required]
        public int ReportType { get; set; }

        [Required]
        public int ReportValue { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class DivisionSalesReportExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DivisionSalesReportExall> data { get; set; }
    }

    public class DivisionSalesReportEx
    {
        public string partynm { get; set; }
        public string exnm { get; set; }
        public string Division { get; set; }

        //   public string AmountinLk { get; set; }
        public string Amount { get; set; }
    }

    public class DivisionSalesReportExall
    {
        public List<DivisionSalesReportEx> DivisionSalesReport { get; set; }

        //   public string TotalAmtinlk { get; set; }
        public string TotalAmt { get; set; }
    }
}