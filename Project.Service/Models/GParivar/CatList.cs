using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
 

    public class ListsofCat
    {
        [Required]
        public string Cin { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }


    }

    public class CatLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CatList> data { get; set; }
    }

    public class CatList
    {
        public int slno { get; set; }
        public string categorynm { get; set; }

    }
}