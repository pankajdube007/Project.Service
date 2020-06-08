using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Project.Service.Models
{
  

    public class ListofExpenseChildAll
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

        [Required]
        public int branchid { get; set; }


    }

    public class ExpenseChildAlls
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExpenseChildAll> data { get; set; }
    }

    public class ExpenseChildAll
    {

        public string name { get; set; }
        public string amount { get; set; }
        public string link { get; set; }
    }
}