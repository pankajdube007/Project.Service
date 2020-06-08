using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Project.Service.Models
{
 
    public class ListofVendorPurOrderAll
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string PartyId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string Todate { get; set; }


    }


    public class VendorPurOrderAlls
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VendorPurOrderAll> data { get; set; }
    }

    public class VendorPurOrderAll
    {
        public string PartyId { get; set; }
        public string TypeOfParty { get; set; }
        public string Party { get; set; }
        public string Branch { get; set; }
        public string BranchId { get; set; }
        public string Total { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Division { get; set; }
        public string ReceivedDate { get; set; }
        public string Fileurl { get; set; }




    }

}