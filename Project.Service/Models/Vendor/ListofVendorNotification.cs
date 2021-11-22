using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofVendorNotification
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int PartyID { get; set; }

        [Required]
        public string Cat { get; set; }
    }
    public class GetVendorNotificationDataDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetVendorNotificationDataDetail> data { get; set; }
    }

    public class GetVendorNotificationDataDetail
    {
        public string Message { get; set; }
        public string createdt { get; set; }
        public int Status { get; set; }
       
    }

}