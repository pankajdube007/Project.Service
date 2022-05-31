using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.IT
{
    
    public class ListofSetApprovalWeeklyOff
    {
       

        [Required]
        public int EmpID { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        public int ApprovedBy { get; set; }

        [Required]
        public string status { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string NotificationId { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string DeviceType { get; set; }

    }

    public class AddSetApprovalWeeklyOffLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddSetApprovalWeeklyOffList> data { get; set; }
    }

    public class AddSetApprovalWeeklyOffList
    {
        public string output { get; set; }
    }
}