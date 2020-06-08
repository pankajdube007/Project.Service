using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofVendorMastManagement
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }


    }

    public class VendorMastManagements
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorMastManagement> data { get; set; }
    }

    public class VendorMastManagement
    {
        public string slno { get; set; }
        public string vendornm { get; set; }
        public string code { get; set; }
    }

}