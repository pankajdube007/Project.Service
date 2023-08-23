using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofManagementTotalSale
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }

    public class ManagementTotalSales
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ManagementTotalSale> data { get; set; }
    }

    public class ManagementTotalSale
    {
        public string branchid { get; set; }
        public string branchnm { get; set; }       
        public string wiringdevices { get; set; }       
        public string lights { get; set; }      
        public string wireandcable { get; set; }
        public string pipesandfittings { get; set; }
        public string mcbanddbs { get; set; }
        public string fan { get; set; }
        public string Automation { get; set; }
        public string healthcare { get; set; }
        public string totalsale { get; set; }
        public string branchcontribution { get; set; }
        public string branchcontributionpercentage { get; set; }
    }
}