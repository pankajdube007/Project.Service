using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofConfirmPaymentRequest
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public string transactionid { get; set; }
        [Required] public string otp { get; set; }

    }

    public class ConfirmPaymentRequest
    {
        [Required] public string freepayTxnId { get; set; }
        [Required] public string otp { get; set; }
    }
}