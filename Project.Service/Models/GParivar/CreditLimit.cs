using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofCreditLimitAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class CreditLimits
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<CreditLimit> data { get; set; }
    }

    public class CreditLimit
    {
        public string Outstanding { get; set; }
        public string CreditLimits { get; set; }
    }
}