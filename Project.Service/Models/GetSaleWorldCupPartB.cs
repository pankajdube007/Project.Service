using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListGetSaleWorldCupPartB
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class PartySaleWorldCupPartBLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<WorldcupsalePartBFinal> data { get; set; }
    }
    public class PartySaleWorldCupPartBList
    {
        public string PartyName { get; set; }
        public string Sale { get; set; }

        public string AmountEarned { get; set; }
        public string AmountEarnedPer { get; set; }
        public string ChancesToWin { get; set; }
        public string ChancesToWinper { get; set; }
        public string Won { get; set; }
        public string Lost { get; set; }
        public string pdfurl { get; set; }
      
    }
    public class WorldcupImgPartBList
    {
        public string imgurl { get; set; }
    }


    public class WorldcupsalePartBFinal
    {
        public List<PartySaleWorldCupPartBList> detail { get; set; }
        public List<WorldcupImgPartBList> img { get; set; }
    }
}