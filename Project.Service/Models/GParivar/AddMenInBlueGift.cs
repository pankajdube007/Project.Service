using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class PartyWiseMeninBlueGiftAdd
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
    }

    public class MeninBlues
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MeninBlue> data { get; set; }
    }

    public class MeninBlue
    {
        public int type { get; set; }
        public string message { get; set; }
    }
}