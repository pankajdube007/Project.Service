using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  


    public class ListofThirdPartyPurOrderStatus
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int DivID { get; set; }
        [Required]
        public string Fromdate { get; set; }
        [Required]
        public string Todate { get; set; }
    }


    public class ThirdPartyPurOrderStatuss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ThirdPartyPurOrderStatus> data { get; set; }
    }

    public class ThirdPartyPurOrderStatus
    {
        public string Branch { get; set; }
        public string PONO { get; set; }
        public string PoDate { get; set; }
        public string Party { get; set; }
        public string PartyId { get; set; }
        public string OrderStatus { get; set; }
        public string Status { get; set; }
        public string Total { get; set; }
        public string Url { get; set; }

    }
}