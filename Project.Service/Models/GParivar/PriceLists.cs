using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofPriceLists
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int Count { get; set; }
    }

    public class PriceListss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PriceListFinal> data { get; set; }
    }

    public class PriceListFinal
    {
        public List<PriceLists> pricelistdata { get; set; }
        public bool ismore { get; set; }
    }

    public class PriceLists
    {
        public string BrandName { get; set; }
        public string RangeName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string fileURL { get; set; }
        public string imgurl { get; set; }
    }
}