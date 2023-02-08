using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListupdatelatlonOrganisation
    {
        [Required]
        public int ExId { get; set; }

        public int EmpType { get; set; }

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

    public class updatelatlonOrganisations
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<updatelatlonOrganisation> data { get; set; }
    }

    public class updatelatlonOrganisation
    {
        public string output { get; set; }
    }
}