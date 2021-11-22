using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  
    public class Listsofcatwisenetlandingmanagement
    {
        [Required]
        public string Cin { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }


    }

    public class catwisenetlandingmanagementLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<catwisenetlandingmanagementList> data { get; set; }
    }

    public class catwisenetlandingmanagementList
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string SubCat { get; set; }
        public string Netvalue { get; set; }
    }
}