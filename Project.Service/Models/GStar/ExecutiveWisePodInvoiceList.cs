using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Models
{
    public class ExecutiveWisePodInvoiceList 
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }

        [Required]
        public int Type { get; set; }

        
        public string Search { get; set; }

       
        public string Cin { get; set; }

        [Required]
        public int Hierarchy { get; set; }


    }

    public class GetPodInvoiceList
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PodInvoiceList> data { get; set; }
    }

    public class PodInvoiceList
    {

        public string ExecutiveName { get; set; }
        public string PartyName { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string TotalAmount { get; set; }
        public string PODDate { get; set; }
    }

}