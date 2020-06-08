using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class IncreaseLimitAction
    {
        [Required]
        public int partyid { get; set; }

        public string searchtxt { get; set; }
    }

    public class IncreaseLimits
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<IncreaseLimit> data { get; set; }
    }

    public class IncreaseLimit
    {
        public string cin { get; set; }
        public string displaynm { get; set; }
    }
}