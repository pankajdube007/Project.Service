using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofNetLanding
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

       
    }

    public class NetLandingLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NetLandingList> data { get; set; }
    }

    public class NetLandingList
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string SubCat { get; set; }
        public decimal Netvalue { get; set; }
    }
}