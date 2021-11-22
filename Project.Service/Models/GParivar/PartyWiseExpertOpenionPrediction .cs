using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListPartyWiseExpertOpenionPrediction
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public int MatchSummaryId { get; set; }
        [Required]
        public int BatFirstTeam { get; set; }
        [Required]
        public int TossWinTeamId { get; set; }
        [Required]
        public int foureid { get; set; }
        [Required]
        public int sixid { get; set; }
        [Required]
        public int scoreid { get; set; }
    }
    public class PartyWiseExpertOpenionPredictionsLits
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyWiseExpertOpenionPredictionList> data { get; set; }
    }

    public class PartyWiseExpertOpenionPredictionList
    {
        public string output { get; set; }
    }
}