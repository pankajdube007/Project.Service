using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofManagementBranchwiseAllTransaction
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

     
    }

    public class ManagementBranchwiseAllTransactions
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ManagementBranchwiseAllTransaction> data { get; set; }
    }

    public class ManagementBranchwiseAllTransaction
    {
        public string branchid { get; set; }
        public string branchnm { get; set; }
        public string salewithtaxamt { get; set; }
        public string salewithouttaxamt { get; set; }
        public string payment { get; set; }
        public string creditnote { get; set; }
        public string debitnote { get; set; }
        public string outstandingamt { get; set; }
        public string stockamt { get; set; }
        public string purchaseamt { get; set; }
    }
}