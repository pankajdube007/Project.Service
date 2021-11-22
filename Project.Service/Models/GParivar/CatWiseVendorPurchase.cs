using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{


    public class ListCatWiseVendorPurchaseList
    {
        [Required]
        public string Cat { get; set; }
       
        [Required]
        public string VendorId { get; set; }
        [Required]
        public string Cin { get; set; }
        [Required]
        public string fromdate { get; set; }
        [Required]
        public string todate { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class CatWiseVendorPurchaseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CatWiseVendorPurchaseList> data { get; set; }
    }
    public class CatWiseVendorPurchaseList
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
    }
}