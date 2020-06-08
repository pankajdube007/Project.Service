using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class SetPasswordAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class SetPassword
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
    }
}