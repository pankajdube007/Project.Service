using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofDcrInputDetails
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class DcrInputDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DcrInputDetailsFinal> data { get; set; }
    }

    public class DcrInputDetailsFinal
    {
        public List<contactmode> contactmodedata { get; set; }
        public List<purposelist> purposelistdata { get; set; }
        public List<productlist> productlistdata { get; set; }
        public List<transportlist> transportlistdata { get; set; }
    }

    public class contactmode
    {
        public int slno { get; set; }
        public string name { get; set; }
    }

    public class purposelist
    {
        public int slno { get; set; }
        public string name { get; set; }
    }

    public class productlist
    {
        public int slno { get; set; }
        public string name { get; set; }
    }

    public class transportlist
    {
        public int slno { get; set; }
        public string name { get; set; }
    }
}