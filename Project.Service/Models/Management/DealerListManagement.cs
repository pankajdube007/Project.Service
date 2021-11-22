using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofDealerListManagement
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }

      
    }

    public class DealerListManagements
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DealerListManagement> data { get; set; }
    }

    public class DealerListManagement
    {
        public string partyid { get; set; }
        public string CIN { get; set; }
        public string displaynm { get; set; }
    }
}