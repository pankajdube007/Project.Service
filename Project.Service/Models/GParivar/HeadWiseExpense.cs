using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofHeadWiseExpense
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string BranchId { get; set; }


        [Required]
        public string fromdate { get; set; }

        [Required]
        public string todate { get; set; }


    }

    public class HeadWiseExpenses
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<HeadWiseExpense> data { get; set; }
    }

    public class HeadWiseExpense
    {
        public string ledgerid { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
    }
}