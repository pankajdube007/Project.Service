using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class DcrOrganisation
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
      
        public int EmpType { get; set; }
    }

    public class DcrOrganisationLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DcrOrganisationList> data { get; set; }
    }

    public class DcrOrganisationList
    {
        public int orgid { get; set; }
        public string orgname { get; set; }
    }
}