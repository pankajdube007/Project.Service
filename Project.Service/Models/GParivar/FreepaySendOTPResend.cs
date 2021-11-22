using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofFreepaySendOTPResend
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
    }

    public class FreepaySendOTPResends
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FreepaySendOTPResend> data { get; set; }
    }
    public class FreepaySendOTPResend
    {
        public bool isRegistered { get; set; }
    }

}