using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofInvoicewiseCD
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string InvoiceNo { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class InvoicewiseCDs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<InvoicewiseCD> data { get; set; }
    }

    public class InvoicewiseCD
    {
        public string duedays { get; set; }
        public string percentage { get; set; }
        public string duedate { get; set; }
    }
}