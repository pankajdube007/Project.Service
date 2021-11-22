using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ExByExAction
    {
        [Required]
        public int slno { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ExByExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExByEx> data { get; set; }
    }

    public class ExByEx
    {
        public int slno { get; set; }
        public string salesexnm { get; set; }
        public string printnm { get; set; }
    }
}