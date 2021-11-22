using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{

    public class Listofvendorpurchaseordrpendingsummary
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

    public class vendorpurchaseordrpendingsummaryLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<vendorpurchaseordrpendingsummaryList> data { get; set; }
    }

    public class vendorpurchaseordrpendingsummaryList
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string SubCat { get; set; }
        public string Color { get; set; }
    
        public string Barnch { get; set; }
        public string TotPendingQty { get; set; }

    }
}