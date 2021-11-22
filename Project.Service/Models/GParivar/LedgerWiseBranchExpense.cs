using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListofLedgerWiseBranchExpense
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string fromdate { get; set; }

        [Required]
        public string todate { get; set; }

        [Required]
        public int ledgerid { get; set; }



    }

    public class LedgerWiseBranchExpenses
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LedgerWiseBranchExpense> data { get; set; }
    }

    public class LedgerWiseBranchExpense
    {
        public string branchid { get; set; }
        public string branchnm { get; set; }
        public string amount { get; set; }
    }
}