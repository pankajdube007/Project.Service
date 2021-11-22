using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofManagementTotalPayment
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }

    public class ManagementTotalPayments
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ManagementTotalPayment> data { get; set; }
    }

    public class ManagementTotalPayment
    {
        public string branchid { get; set; }
        public string branchnm { get; set; }
        public string payment { get; set; }
        public string totalpayment { get; set; }
        public string contribution { get; set; }
    }
}