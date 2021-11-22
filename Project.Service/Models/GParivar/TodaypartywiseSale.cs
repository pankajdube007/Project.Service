using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofTodayPartywiseSale
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string fromdate { get; set; }

        [Required]
        public string todate { get; set; }

        [Required]
        public int Branch { get; set; }

    }

    public class TodayPartywiseSales
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TodayPartywiseSale> data { get; set; }
    }

    public class TodayPartywiseSale
    {
        public string Party { get; set; }
        public string PartyId { get; set; }
        public string TypeCat { get; set; }
  
        public string sale { get; set; }
        public string cin { get; set; }

    }
}