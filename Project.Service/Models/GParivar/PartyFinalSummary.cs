using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListPartyFinalSummary
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }

        [Required]
        public string TeamId { get; set; }
        [Required]
        public string Winner { get; set; }
    }
    public class PartyFinalSummaryLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyFinalSummaryList> data { get; set; }
    }

    public class PartyFinalSummaryList
    {
        public string output { get; set; }
    }
}