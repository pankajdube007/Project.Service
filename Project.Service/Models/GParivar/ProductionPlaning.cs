using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofProductionPlaning
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int SubCategory { get; set; }
    }


    public class ProductionPlanings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ProductionPlaning> data { get; set; }
    }

    public class ProductionPlaning
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string VasaiStock { get; set; }
        public string BranchStock { get; set; }
        public string PurchasePending { get; set; }
        public string AverageSale { get; set; }

    }
}