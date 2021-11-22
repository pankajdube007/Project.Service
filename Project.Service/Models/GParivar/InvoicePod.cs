using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
  
    public class ListsInvoicePod
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Uniquekey { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }

    public class LInvoicePods
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<InvoicePod> data { get; set; }
    }

    public class InvoicePod
    {
        public string Name { get; set; }
        public string InvoiceNo { get; set; }
        public string OrderNo { get; set; }
        public string FinalAmount { get; set; }
        public string LrNO { get; set; }
        public string Transporter { get; set; }
        public string Date { get; set; }
        public string Invoicedate { get; set; }
    }
}