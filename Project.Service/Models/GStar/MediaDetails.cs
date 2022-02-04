using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Models
{
    public class MediaDetails 
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public int Slno { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class MediaAppDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MediaAppDetail> data { get; set; }
    }

    public class MediaAppDetail
    {

        public string Subject { get; set; }
        public string VideoLink { get; set; }
        public string Images { get; set; }
        public string Details { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TimeHH { get; set; }
        public string TimeTT { get; set; }
        public string MediaTypes { get; set; }

    }
}