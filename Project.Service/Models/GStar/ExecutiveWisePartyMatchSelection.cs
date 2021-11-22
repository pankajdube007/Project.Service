using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListExecutiveWisePartyMatchSelection
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string leg { get; set; }

        [Required]
        public string semi { get; set; }

        [Required]
        public string win { get; set; }
    
        public string cin { get; set; }
    }
    public class ExecutiveWisePartyMatchSelectionLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecutiveWisePartyMatchSelectionList> data { get; set; }
    }

    public class ExecutiveWisePartyMatchSelectionList
    {
        public string DealerName { get; set; }
        public string ContactNo { get; set; }
        public string LeagueMatchPrediction { get; set; }
        public string SemiFinalPrediction { get; set; }
        public string WinnerPrediction { get; set; }
        public string SaleAmt { get; set; }
        public string ChanceToWinPer { get; set; }
        public string ChanceToWinAmt { get; set; }
    }
}
