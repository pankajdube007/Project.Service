using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofWareHoseAction
    {
        [Required]
        public string uniquekey { get; set; }

        [Range(1, 1000, ErrorMessage = "User not Valid!!!")]
        public int Branchid { get; set; }
    }

    public class Warehouses
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Warehouse> data { get; set; }
    }

    public class Warehouse
    {
        public int slno { get; set; }
        public string warehousenm { get; set; }
    }
}