using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class Listofvendorpurchasependingorder
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public int cnt { get; set; }
        [Required]
        public int vendorid { get; set; }
    }
    public class vendorpurchasependingorderLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<vendorpurchasependingorderList> data { get; set; }
    }
    public class vendorpurchasependingorderList
    {
        public string ItemId { get; set; }
        public string itemcode { get; set; }
        public string ItemName { get; set; }
        public string ColorName { get; set; }
        public string Subcategory { get; set; }
        public string PendingQty { get; set; }
        public string PendingDays { get; set; }
       


    }
}