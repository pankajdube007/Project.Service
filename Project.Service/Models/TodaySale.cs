using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofTodaySale
    {
        [Required]
        public string ClientSecret { get; set; }
     
        public string CIN { get; set; }
   
        public string Category { get; set; }
    }

    public class TodaySales
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TodaySale> data { get; set; }
    }

    public class TodaySale
    {
        public string today { get; set; }
        public string monthly { get; set; }
        public string quarterly { get; set; }
        public string yearly { get; set; }
    }
}