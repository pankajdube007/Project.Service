using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ListexecutiveGscSalePast
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int Schemeid { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class executiveGscSalePast
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<executiveGscSalesPast> data { get; set; }
    }

    public class executiveGscSalesPast
    {
        public string partyname { get; set; }
        public string itemname { get; set; }
        public string sale { get; set; }

    }

}