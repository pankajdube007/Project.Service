using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListsofAllBranch
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class AllBranchs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AllBranch> data { get; set; }
    }

    public class AllBranch
    {
        public int branchid { get; set; }
        public string branchnm { get; set; }
    }
}