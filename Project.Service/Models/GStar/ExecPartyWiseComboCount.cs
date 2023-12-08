using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
    public class ListsPartyWiseCobmoCount
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int ComboId { get; set; }

    }

    public class PartyWiseCobmoCounts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyWiseCobmoCount> data { get; set; }
    }

    public class PartyWiseCobmoCount
    {
       
        public string Cin { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public string Used { get; set; }
        
    }
}