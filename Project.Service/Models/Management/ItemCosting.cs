using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ItemCosting
    {

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Cat { get; set; }
    }

    public class ListItemCostings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ListItemCosting> data { get; set; }
    }

    public class ListItemCosting
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Division { get; set; }
        public string MRP { get; set; }
        public string OfferPrice { get; set; }
        public string uc { get; set; }
        public string RegularPer { get; set; }
        public string QtyPer { get; set; }
        public string TodPer { get; set; }
        public string MdPer { get; set; }
        public string WdPer { get; set; }
        public string AddPer { get; set; }
        public string BrandLoyaltyPer { get; set; }
        public string CommPer { get; set; }
        public string BranchExpensesPer { get; set; }
        public string MarketingPer { get; set; }
        public string FrieghtPer { get; set; }
        public string SalesoverheadPer { get; set; }
        public string PurchaseoverheadPer { get; set; }
        public string DiscountPer { get; set; }
        public string CDPer { get; set; }
        public string PromotionalPer { get; set; }
        public string PaytmAmt { get; set; }
        public string DiscountAmt { get; set; }
        public string PromotionalAmt { get; set; }
        public string CDAmt { get; set; }
        public string RegularAmt { get; set; }
        public string QtyAmt { get; set; }
        public string TODAmt { get; set; }
        public string MDAmt { get; set; }
        public string WdAmt { get; set; }
        public string AddValue { get; set; }
        public string BranchExpensesvalue { get; set; }
        public string Marketingvalue { get; set; }
        public string BrandLoyaltyvalue { get; set; }
        public string frieghtvalue { get; set; }
        public string commpervalue { get; set; }
        public string salesoverheadvalue { get; set; }
        public string final { get; set; }
        public string purvalue { get; set; }
        public string totalpurvalue { get; set; }
        public string margin { get; set; }
        public string marginper { get; set; }
        


    }
}