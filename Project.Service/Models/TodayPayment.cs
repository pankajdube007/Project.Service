using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofTodayPayment
    {
        [Required]
        public string ClientSecret { get; set; }

        public string CIN { get; set; }

        public string Category { get; set; }
    }

    public class TodayPayments
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TodayPayment> data { get; set; }
    }

    public class TodayPayment
    {
        public string today { get; set; }
        public string monthly { get; set; }
        public string quarterly { get; set; }
        public string yearly { get; set; }
    }
}