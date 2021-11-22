using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class PartyStatusInput
    {

        [Required] public string deviceid { get; set; }
        [Required] public string key { get; set; }
    }

    public class PartyStatuss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyStatus> data { get; set; }
    }

    public class PartyStatus
    {
        public string status { get; set; }
    }

    public class PartyStop
    {

        public string SUBID { get; set; }
    }

    public class Party
    {
        public string SUBID { get; set; }
    }
}