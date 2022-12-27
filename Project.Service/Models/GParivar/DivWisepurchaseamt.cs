using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
   
    public class ListDivWiseVendorPurchaseList
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Finyear { get; set; }
       
    }
    public class DivWiseVendorPurchaseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DivWiseVendorPurchaseList> data { get; set; }
    }
    public class DivWiseVendorPurchaseList
    {
        public string DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string SaleAmount { get; set; }
        public string Amount { get; set; }
    }
}