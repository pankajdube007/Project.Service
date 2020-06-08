using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class TopProductDistSaleAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class TopProductDistSales
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TopProductDistSale> data { get; set; }
    }

    public class TopProductDistSale
    {
        public string rangenm { get; set; }
        public string sale { get; set; }
    }
}