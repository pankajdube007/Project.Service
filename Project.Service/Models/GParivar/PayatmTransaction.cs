using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListPaytmTransaction
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Mobile { get; set; }


        [Required]
        public decimal Amount { get; set; }
     

    }

    public class PaytmTransactions
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PaytmTransaction> data { get; set; }
    }

    public class PaytmTransaction
    {
        public string output { get; set; }
    }
}