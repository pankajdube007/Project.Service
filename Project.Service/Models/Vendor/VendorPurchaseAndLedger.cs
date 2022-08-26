using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   

    public class ListofVendorPurchaseAndLedger
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int vendorid { get; set; }

        [Required]
        public string fromdate { get; set; }

        [Required]
        public string todate { get; set; }


    }

    public class VendorPurchaseAndLedgers
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorPurchaseAndLedger> data { get; set; }
    }

    public class VendorPurchaseAndLedger
    {
       public string purchaseamt { get; set; }
        public string ledgerbalanceamt { get; set; }
        public string saleamt { get; set; }
        public string dayss { get; set; }
        
    }
}