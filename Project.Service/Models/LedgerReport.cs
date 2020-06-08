using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListOfLedgerReport
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class LedgerReportLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LedgerReportList> data { get; set; }
    }

    public class LedgerReportList
    {
        public string Link { get; set; }
    }
}