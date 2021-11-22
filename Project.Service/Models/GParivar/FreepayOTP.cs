using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListsofFreepayOTP
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public string otp { get; set; }
    }

    //public class FreepaySendOTPs
    //{
    //    public bool result { get; set; }
    //    public string message { get; set; }
    //    public string servertime { get; set; }
    //    public List<FreepaySendOTP> data { get; set; }
    //}

    //public class FreepaySendOTP
    //{
    //    public string code { get; set; }
    //    public string message { get; set; }
    //    public decimal description { get; set; }
    //}
}