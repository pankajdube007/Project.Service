using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofCinWiseParty
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class CinWisePartyLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CinWisePartyList> data { get; set; }
    }

    public class CinWisePartyList
    {
        public string PartyName { get; set; }
        public string PartyType { get; set; }
        public string City  { get; set; }
        public string Area { get; set; }
        public string ExecutiveName { get; set; }
        public string Executivephno { get; set; }
        public string ExtraCDDays { get; set; }
    }

}