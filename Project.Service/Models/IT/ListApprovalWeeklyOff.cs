using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.IT
{
   
    public class ListofApprovalWeeklyOff
    {
        [Required]
        public int EmpID { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string NotificationId { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string DeviceType { get; set; }

    }

    public class GetApprovalWeeklyOffLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetApprovalWeeklyOffList> data { get; set; }
    }

    public class GetApprovalWeeklyOffList
    {
        public string EmpID { get; set; }
        public string WeeklyOffDay { get; set; }
        public string ApprovalStatus { get; set; }
        
    }
}