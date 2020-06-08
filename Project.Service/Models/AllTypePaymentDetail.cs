using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListAllTypePaymentDetail
    {
        [Required]
        public int CIN { get; set; }

        [Required]
        public string sdate { get; set; }

        [Required]
        public string edate { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class AllTypePaymentDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AllTypePaymentDetail> data { get; set; }
    }

    public class AllTypePaymentDetail
    {
        public decimal sumofinvoice { get; set; }
        public int noofinvoice { get; set; }
        public decimal sumofdebitnote { get; set; }
        public int noofdebitnote { get; set; }
        public decimal sumofpayment { get; set; }
        public int noofpayment { get; set; }
        public decimal sumofcreditnote { get; set; }
        public int noofcreditnote { get; set; }
        public decimal ledgerbalance { get; set; }
    }
}