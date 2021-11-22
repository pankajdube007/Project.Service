using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   

    public class ValidateVendorPaymentAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ValidateVendorPayments
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ValidateVendorPayment> data { get; set; }
    }

    public class ValidateVendorPayment
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string requestno { get; set; }
        public string otp { get; set; }
    }
}