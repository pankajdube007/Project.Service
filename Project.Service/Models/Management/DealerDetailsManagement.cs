using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListofDealerDetailsManagement
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }


    }

    public class DealerDetailsManagements
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DealerDetailsManagement> data { get; set; }
    }

    public class DealerDetailsManagement
    {
        public string slno { get; set; }
        public string dealernm { get; set; }
        public string cin { get; set; }
    }
}