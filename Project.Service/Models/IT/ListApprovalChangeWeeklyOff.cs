using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.IT
{
    
    public class ListofApprovalChangeWeeklyOff
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

    public class GetApprovalChangeWeeklyOffLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetApprovalChangeWeeklyOffList> data { get; set; }
    }

    public class GetApprovalChangeWeeklyOffList
    {
        public string EmpID { get; set; }
        public string ChangeWeeklyOffDay { get; set; }
        public string ApprovalStatus { get; set; }
        public string salesexnm { get; set; }
        public string EmployeeLastName { get; set; }
    }
}