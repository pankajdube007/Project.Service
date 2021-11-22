using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class GetVendorNotificationCount
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string PartyID  { get; set; }

        [Required]
        public string Cat { get; set; }
       

    }

    public class GetNotificationCountDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetNotificationCountDetail> data { get; set; }
    }

    public class GetNotificationCountDetail
    {
        public int Count { get; set; }
    }

}