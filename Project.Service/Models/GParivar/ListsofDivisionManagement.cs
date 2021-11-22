using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListsofDivisionManagementAction
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
    }

    public class ListsofDivisionManagements
{
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ListsofDivisionManagement> data { get; set; }
    }

    public class ListsofDivisionManagement
{
        public int slno { get; set; }
        public string divisioncode { get; set; }
        public string Divisionnm { get; set; }
    }
}