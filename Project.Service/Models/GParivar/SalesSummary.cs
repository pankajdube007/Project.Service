using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class SalesSummaryAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FinYear { get; set; }

        
        public int exid { get; set; }
    }

    public class SalesSummarys
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SalesSummary> data { get; set; }
    }

    public class SalesSummary
    {
        public List<SalesSummaryDetails> SummaryDetails { get; set; }
        public List<StarRewardurls> StarRewardurl { get; set; }
    }

    public class SalesSummaryDetails
    {
        public string division { get; set; }
        public string lstyrsales { get; set; }
        public string curryrsales { get; set; }
        public string lstyrsalestrade { get; set; }
        public string curryrsalestrade { get; set; }
    }

    public class StarRewardurls
    {
        public string url { get; set; }
    }
}