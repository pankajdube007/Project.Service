using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class VendorRemarkUpdate
    {

        [Required]
        public int HeadID { get; set; }

        [Required]
        public string QrSlno { get; set; }

        [Required]
        public string QrCode { get; set; }

        [Required]
        public int VendorID { get; set; }

        [Required]
        public string VendorRemark { get; set; }

        [Required]
        public int VendorAction { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ItemProblemVendorsUpdate
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemProblemVendorUpdateData> data { get; set; }
    }

    public class ItemProblemVendorUpdateData
    {

        public string itemName { get; set; }
        public string ItemId { get; set; }
        public string qrcode { get; set; }
        public string qrslno { get; set; }
        public string headid { get; set; }

        public string updateon { get; set; }
        public string vendorRemark { get; set; }
        public string action { get; set; }
        public List<ItemProblemVendorUpdate> Issue { get; set; }
    }

    public class ItemProblemVendorUpdate
    {
        public string problemid { get; set; }
        public string problem { get; set; }
        public string remark { get; set; }


    }

    
}