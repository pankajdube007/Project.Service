using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
   

    public class ListApppayment
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string Todate { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class ApppaymentLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ApppaymentList> data { get; set; }
    }
    public class ApppaymentList
    {
        public string slno { get; set; }
        public string AppAmt { get; set; }
        public string Voucherdt { get; set; }
        public string ClearanceAmt { get; set; }
        public string BANKTDATE { get; set; }
        public string PendingAmt { get; set; }
        public string ChequeReturn { get; set; }


    }
}