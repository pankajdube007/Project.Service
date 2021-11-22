using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
    public class ListMatchSummary
    {

        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }

    public class MatchSummaryLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MatchSummaryList> data { get; set; }
    }

    public class MatchSummaryList
    {
        public int Matchsummaryid { get; set; }
        public string Event { get; set; }
        public int Team1id { get; set; }
        public int Team2id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
        public decimal team1point { get; set; }
        public decimal team2point { get; set; }
        public bool winnerlock { get; set; }
        public string winnerteam { get; set; }
        public bool othetplay { get; set; }
        public string othetplayteam { get; set; }
        public decimal team1per { get; set; }
        public decimal team2per { get; set; }
        public string venue { get; set; }
        public int othetplayteamid { get; set; }
        public int winnerlockteamid { get; set; }
        public int selectedteam { get; set; }
        public bool team1selected { get; set; }
        public bool team2selected { get; set; }
        public bool prediction { get; set; }
        public string result { get; set; }
        public bool prediction1 { get; set; }
        public string result1 { get; set; }

     
        public string isSelectionAvailable { get; set; }

    }
}