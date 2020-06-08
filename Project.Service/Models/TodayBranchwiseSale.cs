using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListofTodayBranchwiseSale
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

    }

    public class TodayBranchwiseSales
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TodayBranchwiseSale> data { get; set; }
    }

    public class TodayBranchwiseSale
    {
        public string branchnm { get; set; }
        public string amount { get; set; }
       
    }
}