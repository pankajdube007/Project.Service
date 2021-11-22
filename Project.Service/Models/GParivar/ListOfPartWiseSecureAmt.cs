using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListOfPartyWiseSecureAmt
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }
        [Required]
        public string Branch { get; set; }


    }

    public class partysecureamts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<partysecureamt> data { get; set; }
    }

    public class partysecureamt
    {
        public string Name { get; set; }
        public string Outstanding { get; set; }
        public string Secured { get; set; }
        public string UnSecured { get; set; }
        public string Securedper { get; set; }
        public string UnSecuredper { get; set; }
        public string Insurance { get; set; }
    }
}