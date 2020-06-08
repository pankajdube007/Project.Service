using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class salerequestbillingupdate
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slNo { get; set; }
        [Required] public string lrNo { get; set; }
        [Required] public string lrDate { get; set; }
        [Required] public string docNo { get; set; }
        [Required] public string docDate { get; set; }
        [Required] public int docType { get; set; }
        [Required] public decimal amount { get; set; }
        [Required] public string transporter { get; set; }
        [Required] public string invoiceImage { get; set; }
    }

    public class salerequestbillings
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public object data { get; set; }
    }
}