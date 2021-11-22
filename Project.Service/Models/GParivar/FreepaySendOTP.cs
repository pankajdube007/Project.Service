using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofFreepaySendOTP
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
    }

    public class FreepaySendOTPs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FreepaySendOTP> data { get; set; }
    }
    public class FreepaySendOTP
    {
        public bool isRegistered { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
    }
}