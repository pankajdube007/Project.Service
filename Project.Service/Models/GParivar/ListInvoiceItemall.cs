using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListInvoiceItemall
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string branchid { get; set; }
        

        [Required]
        public string datefrom { get; set; }

        [Required]
        public string dateto { get; set; }

        [Required]
        public bool Ishomebrnch { get; set; }

        [Required]
        public string Cat { get; set; }
    }

    public class GetInvoiceItemallDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetInvoiceItemallDetail> data { get; set; }
    }

    public class GetInvoiceItemallDetail
    {
        public string ponum { get; set; }
        public string hsn { get; set; }
        public string invoicetype { get; set; }
        public string rangenm { get; set; }
        public string invoiceno { get; set; }
        public string invoicedate { get; set; }
        public int partyid { get; set; }
        public int ItemID { get; set; }
        public double disper { get; set; }
        public string unitnm { get; set; }
        public string itemcode { get; set; }
        public string brnch { get; set; }
        public double offerprice { get; set; }
        public double itemmrp { get; set; }
        public double undercutting { get; set; }
        public double taxper { get; set; }
        public double amount { get; set; }
        public double discount { get; set; }
        public double tax { get; set; }
        public double finalamount { get; set; }
        public string PCategory { get; set; }
        public string PartyName { get; set; }
        public string statenm { get; set; }
        public double taxper1 { get; set; }
        public string divisionnm { get; set; }
        public string categorynm { get; set; }
        public string warehousenm { get; set; }
        public string agentnm { get; set; }
        


    }
}