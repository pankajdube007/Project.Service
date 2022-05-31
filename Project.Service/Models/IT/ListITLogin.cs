using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.IT
{
    
    public class ListofITLogin
    {
        
        [Required]
        public string ClientSecret { get; set; }

        
        public string Usernm { get; set; }

        
        public string password { get; set; }

        [Required]
        public string NotificationId { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public Boolean IsRefresh { get; set; }
        

    }

    public class GetITLoginLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetITLoginList> data { get; set; }
    }

    public class GetITLoginList
    {
        public string SlNo { get; set; }
        public string contactno { get; set; }
        public string salesexnm { get; set; }
        public string EmployeeLastName { get; set; }
        public string WeeklyDayOff { get; set; }
        public string Status { get; set; }
        public string IsApproval { get; set; }
        public string IsWeeklyoffset { get; set; }
        
    }
}