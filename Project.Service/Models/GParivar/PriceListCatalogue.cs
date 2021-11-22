using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofPriceListCatalogue
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

    public class PriceListCatalogues
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PriceListCatalogueFinal> data { get; set; }
    }

    public class PriceListCatalogueFinal
    {
        public List<PriceListCatalogue> pricelistdata { get; set; }
        public bool ismore { get; set; }
    }

    public class PriceListCatalogue
    {
        public string BrandName { get; set; }
        public string RangeName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string fileURL { get; set; }
        public string imgurl { get; set; }
    }
}