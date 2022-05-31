using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class PartyWiseBonanazaGiftAdd
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]

        public string  address { get; set; }
        [Required]
        public object PriceDetails { get; set; }
    }

    public class PartyWiseBonanazas
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyWiseBonanaza> data { get; set; }
    }

    public class PartyWiseBonanaza
    {
        public int type { get; set; }
        public string message { get; set; }
    }
}