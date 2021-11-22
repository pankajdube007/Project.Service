using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofAging
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class Agings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Aging> data { get; set; }
    }

    public class Aging
    {
        public List<AgingDeatail> AgingDetails { get; set; }
        public List<Agingurl> Agingurls { get; set; }
    }

    public class AgingDeatail
    {
        public string days { get; set; }
        public string Amount { get; set; }
    }

    public class Agingurl
    {
        public string url { get; set; }
    }
}