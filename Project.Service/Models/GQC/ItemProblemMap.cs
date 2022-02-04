using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
    public class ListofItemProblemMap
    {
        [Required]
        public string QrCode { get; set; }
        [Required]
        public string itemid { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public object ProblemDetails { get; set; }
        [Required]
        public string remark { get; set; }
    }

    public class ItemProblemMaps
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemProblemMap> data { get; set; }
    }

    public class ItemProblemMap
    {
        public int type { get; set; }
        public string message { get; set; }
    }
}