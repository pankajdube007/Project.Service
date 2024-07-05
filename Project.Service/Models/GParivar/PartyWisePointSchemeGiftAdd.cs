using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class PartyWisePointSchemeGiftAdd
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public string address { get; set; }
        
        [Required]
        public object PriceDetails { get; set; }

        [Required]
        public int  SchemeID { get; set; }

    }
    public class PartyWisePointSchemeGiftAddLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyWisePointSchemeGiftAddList> data { get; set; }
    }

    public class PartyWisePointSchemeGiftAddList
    {
        public int type { get; set; }
        public string message { get; set; }
    }
}