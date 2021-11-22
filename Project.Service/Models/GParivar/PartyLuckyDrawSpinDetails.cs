using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
   
    public class ListsofPartyLuckyDrawSpinDetail
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class PartyLuckyDrawSpinDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyLuckyDrawSpinDetail> data { get; set; }
    }

    public class PartyLuckyDrawSpinDetail
    {
        public string Name { get; set; }
        public string TotalChance { get; set; }
        public string Used { get; set; }
        public string Remaining { get; set; }
        public string giftid { get; set; }
        public string resultid { get; set; }

        public string randomno { get; set; }
        public bool androidactive { get; set; }
        public bool isactive { get; set; }
        public bool iosactive { get; set; }
        public string alreadywongift { get; set; }

    }
}