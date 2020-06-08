using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{
    public class ListItemWieMargin
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string ClientSecret { get; set; }


    }

    public class ItemWieMarginLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemWieMarginList> data { get; set; }
    }
    
    public class ItemWieMarginList
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Division { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public decimal MRP { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal UC { get; set; }
        public string dispervalue { get; set; }
        public string schemevalue { get; set; }
        public string cashpervalue { get; set; }
        public string regularpervalue { get; set; }
        public string Qtypervalue { get; set; }
        public string todpervalue { get; set; }
        public string mdpervalue { get; set; }
        public string wdpwervalue { get; set; }
        public string paytmpointvalue { get; set; }
        public string BrandLoyaltyvalue { get; set; }
        public string BranchExpensesvalue { get; set; }
        public string Marketingvalue { get; set; }
        public string frieghtvalue { get; set; }
        public string commpervalue { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal FinalDiscount { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal Margin { get; set; }
    }
}