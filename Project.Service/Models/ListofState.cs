using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofStateAction
    {
        [Required]
        public string uniquekey { get; set; }
    }

    public class states
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<state> data { get; set; }
    }

    public class state
    {
        public int slno { get; set; }
        public string statenm { get; set; }
    }
}