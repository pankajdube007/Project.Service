using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Project.Service.Models
{


    public class ListofRateComparison
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int SubCatId { get; set; }
        [Required]
        public int PartyType  { get; set; }
        [Required]
        public int ItemId { get; set; }

    }


    public class RateComparisons
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<RateComparison> data { get; set; }
    }

    public class RateComparison
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Party { get; set; }
        public string PartyId { get; set; }
        public string PartyType { get; set; }
        public string Mrp { get; set; }
        public string Comparison { get; set; }
    }

}