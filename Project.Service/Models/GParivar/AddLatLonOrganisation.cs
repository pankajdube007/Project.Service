using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListAddLatLonOrganisation
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int OrgId { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public string Lat { get; set; }

        [Required]
        public string Lon { get; set; }

        public string address { get; set; }
    }

    public class AddLatLonOrganisations
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddLatLonOrganisation> data { get; set; }
    }

    public class AddLatLonOrganisation
    {
        public string output { get; set; }
    }
}