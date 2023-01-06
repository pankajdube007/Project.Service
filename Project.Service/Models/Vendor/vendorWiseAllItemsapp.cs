using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.Vendor
{
    public class vendorWiseAllItemsapp
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int PartyId { get; set; }
    }
    public class vendorWiseAllItemsappLists
    {
        public bool result { get; set; }
        public string item { get; set; }
        public string servertime { get; set; }
        public List<vendorWiseAllItemsappList> data { get; set; }
    }
    public class vendorWiseAllItemsappList
    {
        public string ProductCode1 { get; set; }
        public string slno { get; set; }
        

    }
}