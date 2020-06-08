using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class mpininsertlist
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public string category { get; set; }
        [Required] public string newmpin { get; set; }
        public string oldmpin { get; set; }
        [Required] public string deviceId { get; set; }
        [Required] public string appid { get; set; }
    }

    public class mpininserts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<mpininsert> data { get; set; }
    }

    public class mpininsert
    {
        public string output { get; set; }
    }
}