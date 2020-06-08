using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofActiveDealerDevice
    {
        [Required]
        public string CIN { get; set; }
       
        [Required]
        public string ClientSecret { get; set; }
    }

    public class ActiveDealerDevices
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ActiveDealerDevice> data { get; set; }
    }

    public class ActiveDealerDevice
    {
        public string DeviceType { get; set; }
        public string ModalType { get; set; }
        public string DeviceId { get; set; }
        public string LastAcccessTime { get; set; }
    }
}