using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListsofRetryFreepayTransaction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string transactionid { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class RetryFreepayTransactions
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<RetryFreepayTransaction> data { get; set; }
    }

    public class RetryFreepayTransaction
    {
        public string newtransactionid { get; set; }
    }
}