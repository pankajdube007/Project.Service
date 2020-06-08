using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofItemAction
    {
        [Required]
        public string uniquekey { get; set; }

        [Required]
        public string Item { get; set; }

        [Required]
        public int cat { get; set; }
    }

    public class Items
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<item> data { get; set; }
    }

    public class item
    {
        public int slno { get; set; }
        public string itemnm { get; set; }
    }
}