using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofCoupenCodeCheck
    {

        [Required]
        public string barcode { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class CoupenCodeChecks
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CoupenCodeCheck> data { get; set; }
    }

    public class CoupenCodeCheck
    {

      
        public string CoupenCode { get; set; }
        public string CoupenValue { get; set; }
        public string CoupenExpirydt { get; set; }
        public string IsUsed { get; set; }
        public string CustomerCategory { get; set; }
    }
}