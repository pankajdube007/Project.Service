using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofItemByDivision
    {
        [Required]
        public int DivisionId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ItemByDivisions
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemByDivision> data { get; set; }
    }

    public class ItemByDivision
    {
        public int slno { get; set; }
        public string itemnmnm { get; set; }
    }
}