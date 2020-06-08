using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofManagementPendingSaleChild
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int branchid { get; set; }

        [Required]
        public string type { get; set; }


    }

    public class ManagementPendingSaleChilds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ManagementPendingSaleChild> data { get; set; }
    }

    public class ManagementPendingSaleChild
    {
        public string itemnm { get; set; }
        public string qty { get; set; }
        public string amount { get; set; }
    }

}