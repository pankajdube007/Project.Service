using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofExpenseType
    {
        [Required]
        public int ExId { get; set; }
    
        [Required]
        public string ClientSecret { get; set; }
    }

    public class ExpenseTypes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExpenseType> data { get; set; }
    }

    public class ExpenseType
    {
       
        public int slno { get; set; }
        public string expensename { get; set; }
    }
}