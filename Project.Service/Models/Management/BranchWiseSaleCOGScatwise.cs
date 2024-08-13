using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Controllers.Management
{
    public class BranchWiseSaleCOGScatwise
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
        public int Branchid { get; set; }

        [Required]
        public int DivisionId { get; set; }

    }

    public class BranchWiseSaleCOGScatwiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseSaleCOGScatwiseList> data { get; set; }
    }

    public class BranchWiseSaleCOGScatwiseList
    {
        public string categorynm { get; set; }

        public string catid { get; set; }

        public string sale { get; set; }

        public string salemep { get; set; }

        public string rtn { get; set; }

        public string cnamt { get; set; }

       public string Profit { get; set; }
       public string DnAmount { get; set; }

    }
}