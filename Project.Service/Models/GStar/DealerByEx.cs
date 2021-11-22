using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class DealerByExAction
    {
        [Required]
        public int slno { get; set; }

        [Required]
        public int branchid { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class DealerByExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DealerByEx> data { get; set; }
    }

    public class DealerByEx
    {
        public string cin { get; set; }
        public int Exid { get; set; }
        public string dealnm { get; set; }
        public string dealnm2 { get; set; }
    }
}