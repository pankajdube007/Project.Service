using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{


    public class ListMonthwiseempjoin
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class MonthwiseempjoinLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MonthwiseempjoinList> data { get; set; }
    }
    public class MonthwiseempjoinList
    {
       
        public int EmpCount { get; set; }
        public int leave { get; set; }
        public string Month { get; set; }
    }
}