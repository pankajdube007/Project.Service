using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListAppbanner
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class AppbannerLists
    {

        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AppbannerList> data { get; set; }
    }

    public class AppbannerList
    {
        public string url { get; set; }
    }
}