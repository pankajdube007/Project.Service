using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofActiveExecutiveDevice
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ActiveExecutiveDevices
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ActiveExecutiveDevice> data { get; set; }
    }

    public class ActiveExecutiveDevice
    {
        public string DeviceType { get; set; }
        public string ModalType { get; set; }
        public string DeviceId { get; set; }
        public string LastAcccessTime { get; set; }
    }
}