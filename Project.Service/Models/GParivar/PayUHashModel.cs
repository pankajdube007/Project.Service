using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class PayUHashModel
    {
        [Required]
        public int CIN { get; set; }

        [Required]
        public string HashString { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class PayUHashStrings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PayUHashString> data { get; set; }
    }

    public class PayUHashString
    {
        public string Hash { get; set; }

    }
}