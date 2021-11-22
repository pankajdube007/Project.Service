using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
 
    public class Listofvendorsalepndingsummary
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int PartyId { get; set; }

        [Required]
        public int Division { get; set; }





    }

    public class vendorsalepndingsummaryLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<vendorsalepndingsummaryList> data { get; set; }
    }

    public class vendorsalepndingsummaryList
    {
        public string ItemCode { get; set; }
        public string HSN { get; set; }
        public string ItemName { get; set; }
        public string Division { get; set; }
        public string Color { get; set; }
        public string SubCat { get; set; }
        public string Barnch { get; set; }
        public string TotAprQty { get; set; }
        public string TotInvoiceQty { get; set; }
        public string TotCancelQty { get; set; }
        public string TotPendingQty { get; set; }

    }
}
