using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    
    public class ListChangePassword
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string OldPassword { get; set; }

    }

    public class ChangePasswords
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ChangePassword> data { get; set; }
    }

    public class ChangePassword
    {
        public string isResult { get; set; }
    }
}