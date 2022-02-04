using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofTodayDivisionwiseSale
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

    public class TodayDivisionwiseSales
{
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TodayDivisionwiseSale> data { get; set; }
    }

    public class TodayDivisionwiseSale
{
        public string divisionnm { get; set; }
        public string amount { get; set; }
        public int divisionid { get; set; }
        

    }
}