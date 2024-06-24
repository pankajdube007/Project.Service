using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class BranchWiseSaleCOGSsubitemwise
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
        public int branchid { get; set; }

        [Required]
        public int rangeid { get; set; }
    }

    public class BranchWiseSaleCOGSsubitemwiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseSaleCOGSsubitemwiseList> data { get; set; }
    }

    public class BranchWiseSaleCOGSsubitemwiseList
    {
        public string ProductCode { get; set; }

        public string Sale { get; set; }

        public string SaleMep { get; set; }

        public string Rtn { get; set; }

        public string cnamt { get; set; }

        public string Profit { get; set; }


    }
}