using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListofFinYearWisePartyWisepurchase
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Finyear { get; set; }
        [Required]
        public string catId { get; set; }
    }


    public class FinYearWisePartyWisepurchaselists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FinYearWisePartyWisepurchaselist> data { get; set; }
    }

    public class FinYearWisePartyWisepurchaselist
    {
        public string displaynmwitharea { get; set; }
        public string PartyId { get; set; }
        public string TypeCat { get; set; }
        public string Amount { get; set; }
 
    }
}