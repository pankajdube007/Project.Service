using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{


    public class ListCNDivisionWise
    {
        [Required]
        public string Category { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string BranchId { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
    }
    public class CNDivisionWises
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CNDivisionWise> data { get; set; }
    }
    public class CNDivisionWise
    {
        public string InvoiceType { get; set; }
        public string Division { get; set; }
        public string ActualCN { get; set; }
        public string CNAdjusted { get; set; }
        public string Differnece { get; set; }
    }
}