using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class OrderItemDetailsAction
    {
        [Required]
        public string CIN { get; set; }

        public int DivisionId { get; set; }

        public int CategoryId { get; set; }

        public string ItemId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class OrderItemDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OrderItemDetails> data { get; set; }
    }

    public class OrderItemDetails
    {
        public int itemid { get; set; }
        public string CategoryId { get; set; }
        public string categorynm { get; set; }
        public string SubCategoryId { get; set; }
        public string Subcategorynm { get; set; }
        public string DivisionId { get; set; }
        public string divisionnm { get; set; }
        public string itemcode { get; set; }
        public string mrp { get; set; }
        public string dlp { get; set; }
        public string discount { get; set; }
        public string taxtype { get; set; }
        public string taxpercent { get; set; }
        public string pramotionaldiscount { get; set; }
        public string ApproveQty { get; set; }
        public string UnapproveQty { get; set; }
        public string CartoonQty { get; set; }
        public string BoxQty { get; set; }
        public string Unitnm { get; set; }
    }
}