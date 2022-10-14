using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class YSAReportAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }


      
        public int ExecId { get; set; }
    }

    public class YSAReports
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<YSAReport> data { get; set; }
    }

    public class YSAReport
    {
        public List<YSAReportDetails> ysadetails { get; set; }
        public List<YSAReportTotal> ysatotal { get; set; }
    }

    public class YSAReportDetails
    {
        public string groupnm { get; set; }
        public string ysa { get; set; }
        public string sale { get; set; }
    }

    public class YSAReportTotal
    {
        public string totalsale { get; set; }
    }
}