using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class GetFreePayPaymentList
    {
        [Required] public int CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public string fromdate { get; set; }
        [Required] public string todate { get; set; }
    }

    public class GetFreePayPayments
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetFreePayPayment> data { get; set; }
    }

    public class GetFreePayPayment
    {
        public int slno { get; set; }
        public string Receipt { get; set; }
        public string Voucherdt { get; set; }
        public decimal discoumtamt { get; set; }
        public string status { get; set; }
        public int statusflag { get; set; }
        public decimal totalamt { get; set; }
        public decimal savedamt { get; set; }
        public string transactionid { get; set; }
        public string freepaytransactionid { get; set; }     
        public bool retry { get; set; }     
        
    }
}