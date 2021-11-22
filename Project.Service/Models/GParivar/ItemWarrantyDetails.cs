using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   

    public class ListofItemWarrantyDetails
    {
       
        [Required]
        public int slno { get; set; }

        [Required]
        public string barcode { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public bool IsMaster { get; set; }

    }

    public class ItemWarrantyDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemWarrantyDetails> data { get; set; }
    }

    public class ItemWarrantyDetails
    {

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public string ModelNo { get; set; }
        public string ManufactureDate { get; set; }
        public string MonthOfManufacture { get; set; }
        public string VendorName { get; set; }
        public string LabelGenerationDate { get; set; }
        public string Warrantydate { get; set; }
        public string Qty { get; set; }
    }
}