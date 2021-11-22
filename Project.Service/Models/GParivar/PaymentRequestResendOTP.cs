using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class PaymentRequestResendOTP
    {

        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public string transactionid { get; set; }
    }
}