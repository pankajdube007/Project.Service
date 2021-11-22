using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{


    public class ListofPurAndSalePartyLedger
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
        [Required]
        public string FinYear { get; set; }




    }


    public class PurAndSalePartyLedgers
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PurAndSalePartyLedger> data { get; set; }
    }

    public class PurAndSalePartyLedger
    {
        public string PurchasePartyName { get; set; }
        public string PurchasePartyId { get; set; }
        public string PurchasePartyTypeCat { get; set; }
        public string PurchaseLedgerAmt { get; set; }
        public string SaleLedgerAmt { get; set; }
        public string Jv { get; set; }
        public string Payment { get; set; }
        public string Diffrence { get; set; }
     
       




    }
}