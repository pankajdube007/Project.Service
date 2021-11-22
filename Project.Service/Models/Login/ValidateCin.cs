using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ValidateCinAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ValidateCin
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ValidateCins> data { get; set; }
    }

    public class ValidateCins
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}