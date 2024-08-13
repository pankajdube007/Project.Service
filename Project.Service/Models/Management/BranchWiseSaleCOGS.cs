using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class BranchWiseSaleCOGS
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


    }
    public class BranchWiseSaleCOGSLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseSaleCOGSList> data { get; set; }
    }
    public class BranchWiseSaleCOGSList
    {
        public string DivisioName { get; set; }

        public string Sale { get; set; }

        public string SaleMep { get; set; }

        public string Rtn { get; set; }

        public string cnamt { get; set; }

        public string Profit { get; set; }

        public string DivId { get; set; }
        public string DnAmount { get; set; }

    }
}