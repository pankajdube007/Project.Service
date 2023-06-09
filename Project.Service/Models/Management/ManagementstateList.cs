using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ManagementstateList
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Category { get; set; }
    }

    public class ManagementstateLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<dataManagementstateList> data { get; set; }
    }

    public class dataManagementstateList
    {
        public int Slno { get; set; }

        public string Statename { get; set; }
    }
}