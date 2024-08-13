using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class BranchWiseSaleCOGSpartyWise
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
        public int DivId { get; set; }
    }

    public class BranchWiseSaleCOGSpartyWiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchWiseSaleCOGSpartyWiseList> data { get; set; }
    }

    public class BranchWiseSaleCOGSpartyWiseList
    {
        public string PartyName { get; set; }

        public string Sale { get; set; }

        public string SaleMep { get; set; }

        public string Rtn { get; set; }

        public string cnamt { get; set; }

        public string Profit { get; set; }
        public string DnAmount { get; set; }


    }
}