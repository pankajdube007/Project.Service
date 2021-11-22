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
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string Subcat { get; set; }
        public string Cat { get; set; }
        public string Division { get; set; }
        public string LastPurchaseval { get; set; }
        public string PurchaseAmount { get; set; }
        public string PurchaseOverHead { get; set; }
        public string TotalPurchase { get; set; }
        public string Mrp { get; set; }
        public string UC { get; set; }
        public string DLP { get; set; }
        public string DiscAmt { get; set; }
        public string PromotionalScheme { get; set; }
        public string CD { get; set; }
        public string Regular { get; set; }
        public string Qty { get; set; }
        public string TOD { get; set; }
        public string MD { get; set; }
        public string WD { get; set; }
        public string PaytmPoint { get; set; }
        public string BrandLoyalty { get; set; }
        public string Commission { get; set; }
        public string BranchExpenses { get; set; }
        public string Marketing { get; set; }
        public string Frieght { get; set; }
        public string PaytmAmount { get; set; }
        public string SaleOverHead { get; set; }
        public string FinalAmount { get; set; }
        public string FinalDiscount { get; set; }
        public string Margin { get; set; }
        public string MarginPer { get; set; }



    }
}