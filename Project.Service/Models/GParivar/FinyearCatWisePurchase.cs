using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofFinyearCatWisePurchase
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
        public string Div { get; set; }

    }


    public class FinyearCatWisePurchaselists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FinyearCatWisePurchaselist> data { get; set; }
    }

    public class FinyearCatWisePurchaselist
    {
        public string CatId { get; set; }
        public string CatName { get; set; }
        public string Amount { get; set; }
    }
}
