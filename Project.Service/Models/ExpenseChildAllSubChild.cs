using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofExpenseChildAllSubChild
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

        [Required]
        public string suppliername { get; set; }

    }

    public class ExpenseChildAllSubChilds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExpenseChildAllSubChild> data { get; set; }
    }

    public class ExpenseChildAllSubChild
    {

        public string vocherno { get; set; }
        public string date { get; set; }
        public string amount { get; set; }
        public string narration { get; set; }
        public string paymentmode { get; set; }
        public string type { get; set; }
        public string link { get; set; }
    }
}