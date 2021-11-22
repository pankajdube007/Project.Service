using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{
   
    public class ListCatWiseItemMargin
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }


    }

    public class CatWiseItemMarginLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CatWiseItemMarginList> data { get; set; }
    }

    public class CatWiseItemMarginList
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string LastPerValue { get; set; }
        public string TotalPurValue { get; set; }
        public string OfferPrice { get; set; }
        public string FinalAmount { get; set; }
        public string FinalDiscount { get; set; }
        public string Margin { get; set; }
        public string MarginPer { get; set; }
    }
}