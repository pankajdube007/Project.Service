using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofDateWiseSale
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
        public string type { get; set; }

    }

    public class DateWiseSales
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DateWiseSale> data { get; set; }
    }

    public class DateWiseSale
    {
        public string Date { get; set; }
        public string Amount { get; set; }

    }
}