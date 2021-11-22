using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class Listofgettcs
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

    }

    public class gettcss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<gettcs> data { get; set; }
    }

    public class gettcs
    {
        public string companynm { get; set; }
        public string companypan { get; set; }
        public string emailid { get; set; }
        public bool filled { get; set; }
    }
}