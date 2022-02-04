using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofStockAvailibilityEx
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public decimal Qty { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class StockAvailibilityExs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<StockAvailibilityEx> data { get; set; }
    }

    public class StockAvailibilityEx
    {
        public string Branch { get; set; }
        public string AvailableStock { get; set; }
        public string stock { get; set; }
    }
}