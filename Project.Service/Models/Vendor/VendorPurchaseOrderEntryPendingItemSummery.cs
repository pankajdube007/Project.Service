using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{
    
    public class ListofVendorPurchaseOrderEntryPendingItemSummery
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int Division { get; set; }

        [Required]
        public string ApprovedDt { get; set; }

        [Required]
        public int PartyID { get; set; }

        [Required]
        public string branchIDs { get; set; }

        [Required]
        public string Cat { get; set; }

    }

    public class VendorPurchaseOrderEntryPendingItemSummerys
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorPurchaseOrderEntryPendingItemSummery> data { get; set; }
    }

    public class VendorPurchaseOrderEntryPendingItemSummery
    {
        public string HSN { get; set; }
        public string ProductCode1 { get; set; }
        public string itemnm { get; set; }
        public string colornm { get; set; }
        public string rangenm { get; set; }
        public string pendingQty { get; set; }
        public string approvqty { get; set; }
        public string cancelqty { get; set; }
        public string invoiceQty { get; set; }
        public string BranchNm { get; set; }
        public string PartyNm { get; set; }
        public string divisionnm { get; set; }
        public string branchid { get; set; }
        public string partyid { get; set; }
        public string itemid { get; set; }
        public string divisionid { get; set; }



    }
}