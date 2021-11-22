using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Project.Service.Models
{

    public class Listofmonthwisevendorsaleandpurchase
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Vendor { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Finyear { get; set; }




    }


    public class monthwisevendorsaleandpurchaselists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<monthwisevendorsaleandpurchaselist> data { get; set; }
    }

    public class monthwisevendorsaleandpurchaselist
    {
        public string Month { get; set; }
        public string Sale { get; set; }
        public string Purchase { get; set; }
        public string Diffrence { get; set; }
        public string Jv { get; set; }
        public string Payment { get; set; }





    }
}