using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofActiveDeviceLogoutExecutive
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ActiveDeviceLogoutExecutives
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ActiveDeviceLogoutExecutive> data { get; set; }
    }

    public class ActiveDeviceLogoutExecutive
    {
        public string password { get; set; }
    }

}