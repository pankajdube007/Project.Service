using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class BonanzaPriceList
    {
        

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class ListBonanzaPrices
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ListBonanzaPrice> data { get; set; }
    }

    public class ListBonanzaPrice
    {
        public int PriceId { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public string DealerPoint { get; set; }
        public string priceimg { get; set; }
        public string ProductPoint { get; set; }




    }
}