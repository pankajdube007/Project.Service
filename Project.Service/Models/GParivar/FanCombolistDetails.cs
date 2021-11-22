using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListofFanCombolistDetails
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string slno { get; set; }

    }

    public class FanCombolistDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FanCombolistDetailsList> data { get; set; }
    }

    public class FanCombolistDetailsList
    {
        public string categorynm { get; set; }
        public string noofpieces { get; set; }


    }
}