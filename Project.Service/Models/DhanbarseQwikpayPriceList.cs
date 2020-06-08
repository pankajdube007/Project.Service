using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
   
    public class ListsofDhanbarseQwikpayPriceList
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int Type { get; set; }
    }

    public class DhanbarseQwikpayPriceLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DhanbarseQwikpayPriceListFinal> data { get; set; }
    }

    public class DhanbarseQwikpayPriceListFinal
    {
        public List<DhanbarseQwikpayPriceList> pricelistdata { get; set; }
        public bool ismore { get; set; }
    }

    public class DhanbarseQwikpayPriceList
    {
        public string PolicyType { get; set; }
        public string PolicyName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string fileURL { get; set; }
        public string imgurl { get; set; }
    }
}
