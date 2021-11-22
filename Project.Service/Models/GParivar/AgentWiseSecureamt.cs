using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Project.Service.Models
{
    public class Listofagentwisesecureamt
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
    }


    public class agentwisesecureamts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<agentwisesecureamt> data { get; set; }
    }

    public class agentwisesecureamt
    {
        public string Agent { get; set; }
        public string AgentId { get; set; }
        public decimal Outstanding { get; set; }
        public decimal Secured { get; set; }
        public decimal UnSecured { get; set; }
        public decimal Securedper { get; set; }
        public decimal UnSecuredper { get; set; }
        public decimal AgentLimit { get; set; }
    }
}