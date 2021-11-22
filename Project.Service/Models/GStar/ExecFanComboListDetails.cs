using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListofExecFanCombolistDetails
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public int Exec { get; set; }
        [Required]
        public string slno { get; set; }

    }

    public class ExecFanCombolistDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecFanCombolistDetailsList> data { get; set; }
    }

    public class ExecFanCombolistDetailsList
    {
        public string categorynm { get; set; }
        public string noofpieces { get; set; }


    }
}