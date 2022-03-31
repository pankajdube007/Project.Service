using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class CategoryCosting
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Cat { get; set; }
    }


    public class ListCategoryCostings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ListCategoryCosting> data { get; set; }
    }

    public class ListCategoryCosting
    {
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Division { get; set; }
        public string MRP { get; set; }
        public string OfferPrice { get; set; }
        public string FinalAmount { get; set; }
        public string PurchaseValue { get; set; }
        public string TotalPurchaseValue { get; set; }
        public string Margin { get; set; }
        public string MarginPer { get; set; }

        
       

    }
}