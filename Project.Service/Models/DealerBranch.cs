using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class DealerBranchAction
    {

        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }

    public class DealerBranchs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DealerBranch> data { get; set; }

    }
    public class DealerBranch
    {
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }


    }

}