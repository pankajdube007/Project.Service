using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofManagementBranchwiseOuststandingChilld
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int branchid { get; set; }
    }

    public class ManagementBranchwiseOuststandingChillds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ManagementBranchwiseOuststandingChilld> data { get; set; }
    }

    public class ManagementBranchwiseOuststandingChilld
    {
        public string partynm { get; set; }
        public string cin { get; set; }
        public string partystatus { get; set; }
        public string city { get; set; }
        public string lstinvoicedt { get; set; }
        public string lstpaymentdt { get; set; }
        public string lstpaymentamt { get; set; }
        public string outstandingamt { get; set; }
    }
}