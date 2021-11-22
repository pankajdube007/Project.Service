using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class OrderCatItemDetailsAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public int DivisionId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class OrderCatItemDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OrderCatItemDetails> data { get; set; }
    }

    public class OrderCatItemDetails
    {
        public string CategoryId { get; set; }
        public string categorynm { get; set; }
        public string SubCategoryId { get; set; }
        public string Subcategorynm { get; set; }
        public string DivisionId { get; set; }
        public string divisionnm { get; set; }
        public string itemcode { get; set; }
        public string itemnm { get; set; }
        public string Subcategoryurl { get; set; }
    }
}