using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class AppExecutiveEnquiryResponse
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int SlNo { get; set; }

        [Required]
        public string Status { get; set; }


        [Required]
        public string Response { get; set; }


        [Required]
        public string ClientSecret { get; set; }

    }

    public class ExecutiveEnquiryResponse
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public string data { get; set; }
    }
}