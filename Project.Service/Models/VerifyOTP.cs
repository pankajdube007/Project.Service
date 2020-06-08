using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class VerifyOTPAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ReqNo { get; set; }

        [Required]
        public string OTP { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class VerifyOTP
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
    }
}