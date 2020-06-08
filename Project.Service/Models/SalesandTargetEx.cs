using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofSalesandTargetEx
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class SalesandTargetExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SalesandTargetEx> data { get; set; }
    }

    public class SalesandTargetEx
    {
        public string sales { get; set; }
        public string target { get; set; }
    }
}