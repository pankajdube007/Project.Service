using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{
    

    public class ListofMonthWiseOrderSummary
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string DivID { get; set; }
        [Required]
        public string Fromdate { get; set; }
        [Required]
        public string Todate { get; set; }
        [Required]
        public int PatyId { get; set; }
    }


    public class MonthWiseOrderSummarys
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MonthWiseOrderSummary> data { get; set; }
    }

    public class MonthWiseOrderSummary
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string OpeningBal { get; set; }
        public string TotalOrder { get; set; }
        public string TotalReceived { get; set; }
        public string Bal { get; set; }
       

    }
}