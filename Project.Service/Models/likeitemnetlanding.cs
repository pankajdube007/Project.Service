using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{


    public class Listslikeitemnetlanding
    {
        [Required]
        public int ExId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string ClientSecret { get; set; }


    }

    public class likeitemnetlandings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<likeitemnetlanding> data { get; set; }
    }

    public class likeitemnetlanding
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string CatName { get; set; }
        public string Subcatname { get; set; }
        public decimal NetLanding { get; set; }

    }
}