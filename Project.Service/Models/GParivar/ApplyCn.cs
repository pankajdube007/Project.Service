using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofApplyCn
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Amount { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ApplyCns
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ApplyCn> data { get; set; }
    }

    public class ApplyCn
    {
        public string output { get; set; }
    }
}