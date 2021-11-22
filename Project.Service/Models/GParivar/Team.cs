using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListTeam
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
     
    }
    public class TeamLists
    {

        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TeamList> data { get; set; }
    }

    public class TeamList
    {
        public string EventName { get; set; }
        public string TeamName { get; set; }
        public int EventId { get; set; }
        public int TeamId { get; set; }
        public int issemifinal { get; set; }
        public int isfinal { get; set; }
        public int iswinner { get; set; }
        public decimal point { get; set; }
        public int ResultSemifinal { get; set; }
        public int ResultWinner { get; set; }
        public string isSelectionAvailable { get; set; }

    }
}