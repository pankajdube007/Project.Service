using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class CashDiscountAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class CashDiscounts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CashDiscount> data { get; set; }
    }

    public class CashDiscount
    {
        public string cnno { get; set; }
        public string cndate { get; set; }
        public string cnamt { get; set; }
    }
}