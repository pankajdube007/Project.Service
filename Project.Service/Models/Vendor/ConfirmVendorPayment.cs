using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofConfirmVendorPayment
    {
        [Required] public string CIN { get; set; }
        [Required] public string Category { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public string transactionid { get; set; }
        [Required] public string bankid { get; set; }
    }
}