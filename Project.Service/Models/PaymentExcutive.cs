using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofPaymentExcutive
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class PaymentExcutives
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PaymentExcutive> data { get; set; }
    }

    public class PaymentExcutive
    {
        public string division { get; set; }
        public string payment { get; set; }
        public string target { get; set; }
    }
}