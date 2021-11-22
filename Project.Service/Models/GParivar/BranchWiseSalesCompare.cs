using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofBranchWiseSalesCompare
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }
       
    }

    public class BranchWiseSalesCompares
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseSalesCompare> data { get; set; }
    }

    public class BranchWiseSalesCompare
    {
        public string branchid { get; set; }
        public string branchnm { get; set; }
        public string currentyearsale { get; set; }
        public string previousyearsale { get; set; }
        public string previoustwoyearsale { get; set; }
    }

}