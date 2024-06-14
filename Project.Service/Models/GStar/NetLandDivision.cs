using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class NetLandDivision
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class NetLandDivisionLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NetLandDivisionList> data { get; set; }
    }

    public class NetLandDivisionList
    {
        public int Slno { get; set; }
        public string divisioncode { get; set; }
    }
}