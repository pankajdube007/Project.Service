using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListPartySaleWorldCup
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class PartySaleWorldCupLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<WorldcupsaleFinal> data { get; set; }
    }
    public class PartySaleWorldCupList
    {
        public string PartyName { get; set; }
        public string Sale { get; set; }
        
        public string AmountEarned { get; set; }
         public string AmountEarnedPer { get; set; }

        public string AmountEarned1 { get; set; }
        public string AmountEarnedPer1 { get; set; }

        public string ChancesToWin { get; set; }
        public string ChancesToWinper { get; set; }
        public string MatchWon { get; set; }
        public string MatchLost { get; set; }

        public string MatchWon1 { get; set; }
        public string MatchLost1 { get; set; }

        public string MatchBal { get; set; }
        public string MatchBal1 { get; set; }

        public string pdfurl { get; set; }
        public string totalmatch { get; set; }
    }
    public class WorldcupImgList
    {
        public string imgurl { get; set; }
    }


    public class WorldcupsaleFinal
    {
        public List<PartySaleWorldCupList> detail { get; set; }
        public List<WorldcupImgList> img { get; set; }
    }
}