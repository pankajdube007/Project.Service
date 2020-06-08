using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofBranshWiseNonMovementStock
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class BranshWiseNonMovementStocks
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranshWiseNonMovementStock> data { get; set; }
    }

    public class BranshWiseNonMovementStock
    {
        public string branchid { get; set; }
        public string branchnm { get; set; }
        public string stockamt { get; set; }
        public string asondt { get; set; }
        public string slab30 { get; set; }
        public string slab60 { get; set; }
        public string slab90 { get; set; }
        public string slab120 { get; set; }
        public string slab150 { get; set; }
        public string slab180 { get; set; }
        public string slab200 { get; set; }
    }

}