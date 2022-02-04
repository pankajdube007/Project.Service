using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Models
{
    public class ItemListVendor 
    {
        [Required]
        public string QrSlno { get; set; }

        [Required]
        public string QrCode { get; set; }

        [Required]
        public string VendorID { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ListVendor
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorData> data { get; set; }

    }

    public class VendorData
    {
       
        public string itemName { get; set; }
        public string ItemId { get; set; }
        public string qrcode { get; set; }
        public string qrslno { get; set; }
        public string headid { get; set; }
        public List<ProblemListData> Issue { get; set; }
    }

    public class ProblemListData
    {
        public string problemid { get; set; }
        public string problem { get; set; }
        public string remark { get; set; }


    }
}