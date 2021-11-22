using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListsofVendorBankDetails
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
      
    }

    public class VendorBankDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorBankDetail> data { get; set; }
    }
    public class VendorBankDetail
    {
        public string bankid { get; set; }
        public string bankname { get; set; }
        public string ifsccode { get; set; }
        public string accountno { get; set; }
        public string accounttype { get; set; }
        public string codeno { get; set; }



    }

}