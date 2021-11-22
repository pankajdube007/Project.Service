using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{

    public class ListsofItemWiseNetlandingmanagement
    {
        [Required]
        public string Cin { get; set; }

        [Required]
        public int Itemid { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }


    }

    public class ItemWiseNetlandinggmanagementLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemWiseNetlandinggmanagementList> data { get; set; }
    }

    public class ItemWiseNetlandinggmanagementList
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal MRP { get; set; }
        public decimal UC { get; set; }
        public decimal DLP { get; set; }
        public decimal BasicDiscountper { get; set; }
        public decimal BasicDiscountAmount { get; set; }
        public decimal AfterBasicDiscount { get; set; }
        public decimal PromotionalDiscountper { get; set; }
        public decimal PromotionalDiscountAmount { get; set; }
        public decimal AfterPromotionalDiscount { get; set; }
        public decimal Taxper { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal BillWithTax { get; set; }
        public decimal Regularper { get; set; }
        public decimal RegularAmt { get; set; }
        public decimal AfterRegularScheme { get; set; }
        public decimal Qtyper { get; set; }
        public decimal QtyAmt { get; set; }
        public decimal AfterQtyScheme { get; set; }
        public decimal Cashper { get; set; }
        public decimal CashAmt { get; set; }
        public decimal AfterCash { get; set; }
        public decimal Todper { get; set; }
        public decimal TodAmount { get; set; }
        public decimal AfterTod { get; set; }
    }
}