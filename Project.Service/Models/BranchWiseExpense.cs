using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofBranchWiseExpense
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


    }

    public class BranchWiseExpenses
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseExpense> data { get; set; }
    }

    public class BranchWiseExpense
    {
        public string branchid { get; set; }
        public string branchnm { get; set; }
        public string salary { get; set; }
        public string otherespenses { get; set; }
        public string sale { get; set; }
    }
}