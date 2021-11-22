using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListPartyMatchSelection
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string MatchSummaryId { get; set; }
        [Required]
        public string TeamId { get; set; }
    }
    public class PartyMatchSummaryLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyMatchSummaryList> data { get; set; }
    }

    public class PartyMatchSummaryList
    {
        public string output { get; set; }
    }

}