using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Models
{
    public class ExecutiveWisePodCount 
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }

        
        public string Cin { get; set; }

        [Required]
        public int Hierarchy { get; set; }



    }

    public class GetPodCount
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PodCountData> data { get; set; }
    }

    public class PodCountData
    {

        public int SlNo { get; set; }
        public string ExecutiveName { get; set; }
        public int TotalCount { get; set; }
        public int PodCount { get; set; }
        public int Pending { get; set; }

    }

}