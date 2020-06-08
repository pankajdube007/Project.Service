using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofSalesExecutive
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class SalesExecutives
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SalesExecutiveFinal> data { get; set; }
    }

    public class SalesExecutiveFinal
    {
        public List<SalesExecutive> SalesExecutiveDetails { get; set; }
        public string TotalSale { get; set; }
        public string TotalTarget { get; set; }
    }

    public class SalesExecutive
    {
        public string division { get; set; }
        public string sales { get; set; }
        public string target { get; set; }
    }
}