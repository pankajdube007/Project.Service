using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class SchemeExAction
    {
        [Required]
        public string ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Type { get; set; }
    }

    public class SchemeExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SchemeEx> data { get; set; }
    }

    public class SchemeEx
    {
        public string partynm { get; set; }
        public string schemename { get; set; }
        public string netsale { get; set; }
        public string curslab { get; set; }
        public string nextslab { get; set; }
    }
}