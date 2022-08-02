using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    
    public class ListofCFSInvoiceDetails
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Amount { get; set; }
        
    }

    public class GetCFSInvoiceLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetCFSInvoiceList> data { get; set; }
    }

    public class GetCFSInvoiceList
    {
        public string InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string Division { get; set; }
        public string Amount { get; set; }
        public string DueDays { get; set; }
        
    }
}