using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ForgetMpinAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required] public string deviceId { get; set; }
        [Required] public string appid { get; set; }
    }

    public class ForgetMpins
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ForgetMpin> data { get; set; }
    }

    public class ForgetMpin
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string RequestNo { get; set; }
        public string OTP { get; set; }
        // public string LastPin { get; set; }
    }
}