using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofOrganation
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int AreaId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

         public string img { get; set; }
    }

    public class Organations
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Organation> data { get; set; }
    }

    public class Organation
    {
        public int OrgId { get; set; }
    }
}