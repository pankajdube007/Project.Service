using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class AppdealerStockAvailibility
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }
        
        [Required]
        public decimal Qty { get; set; }
        
        [Required]
        public int ItemID { get; set; }
    }
    public class AppdealerStockAvailibilityLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AppdealerStockAvailibilityList> data { get; set; }
    }
    public class AppdealerStockAvailibilityList
    {
        public string TotalQty { get; set; }
        public string Locnm { get; set; }
        public string Stock { get; set; }
    }
}