using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofManagementBranchwiseOuststanding
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }


    }

    public class ManagementBranchwiseOuststandings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ManagementBranchwiseOuststanding> data { get; set; }
    }

    public class ManagementBranchwiseOuststanding
    {
        public string branchid { get; set; }
        public string branchnm { get; set; }
        public string outstandingamt { get; set; }
    }
}