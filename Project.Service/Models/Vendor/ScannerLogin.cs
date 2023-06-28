using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Vendor
{
    
    public class ScannerLogintList
    {
        [Required]
        public string usernam { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class ScannerLoginS
    {   
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ScannerLogin> data { get; set; }
    }


    public class ScannerLogin
    {
        public string host { get; set; }

        public string database { get; set; }

        public string port { get; set; }

        public string user { get; set; }

        public string pass { get; set; }


    }

}