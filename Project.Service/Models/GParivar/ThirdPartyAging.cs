using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListofThirdPartyAging
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Date { get; set; }
        

    }


    public class ThirdPartyAgings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ThirdPartyAging> data { get; set; }
    }

    public class ThirdPartyAging
    {
        public string PartyId { get; set; }
        public string Party { get; set; }
        public string To30Days { get; set; }
        public string To60Days { get; set; }
        public string To90Days { get; set; }
        public string To120Days { get; set; }
        public string To150Days { get; set; }
        public string Ab150Days { get; set; }
        public string total { get; set; }

    }
}