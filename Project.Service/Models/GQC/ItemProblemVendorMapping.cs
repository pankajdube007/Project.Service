using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ItemProblemVendorMapping
    {
        [Required]
        public object QrCode { get; set; }

        [Required]
        public int VendorID { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class ItemProblemVendors
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemProblemVendor> data { get; set; }
    }

    public class ItemProblemVendor
    {
      
        public string output { get; set; }
        public int TotalRecord { get; set; }
        public int UpdatedRecord { get; set; }
    }
}