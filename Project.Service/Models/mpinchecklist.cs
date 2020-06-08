using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class mpinchecklist
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        public string newmpin { get; set; }
        [Required] public string deviceid { get; set; }
        [Required] public string appid { get; set; }
    }

    public class mpinchecks
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<mpincheck> data { get; set; }
    }

    public class mpincheck
    {
        public bool isBlock { get; set; }
        public bool isForcedLogout { get; set; }
    }
}