using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class BranchWiseSaleCOGSBranchWise
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
        public int DivID { get; set; }

    }

    public class BranchWiseSaleCOGSBranchWiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseSaleCOGSBranchWiseList> data { get; set; }
    }

    public class BranchWiseSaleCOGSBranchWiseList
    {
        public string Branch { get; set; }

        public string BranchId { get; set; }

        public string DivId { get; set; }

        public string Sale { get; set; }

        public string SaleMep { get; set; }

        public string Rtn { get; set; }

        public string cnamt { get; set; }

        public string Profit { get; set; }
        public string DnAmount { get; set; }


    }
}