using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    
    public class ListofDivisionBrachWiseSaleExpense
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string branchid { get; set; }

        [Required]
        public string divisiononid { get; set; }

        [Required]
        public string finyear { get; set; }

        [Required]
        public string Qtr { get; set; }
        

    }

    public class DivisionBrachWiseSaleExpenseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DivisionBrachWiseSaleExpenseList> data { get; set; }
    }

    public class DivisionBrachWiseSaleExpenseList
    {
        public string homebranchid { get; set; }
        public string HomeBranch { get; set; }
        public string Sale { get; set; }
        public string DivId { get; set; }
        public string DivisionName { get; set; }
        public string salexp { get; set; }
    }
}