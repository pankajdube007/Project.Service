using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class LatLogOrganisation
    {
        [Required]
        public int ExId { get; set; }


        public int EmpType { get; set; }


        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Orgid { get; set; }

        [Required]
        public int Catid { get; set; }
    }

    public class LatLonOrganisationLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LatLonOrganisationList> data { get; set; }
    }

    public class LatLonOrganisationList
    {
        public string lat { get; set; }
        public string lon { get; set; }
        public string ischeckin { get; set; }
    }
}