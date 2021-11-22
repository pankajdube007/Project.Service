using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofEnquiryAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Subject { get; set; }

        [Required]
        public string EquiryText { get; set; }
    }

    public class Enquirys
    {
        public bool result { get; set; }
        public string servertime { get; set; }
        public string message { get; set; }
    }
}