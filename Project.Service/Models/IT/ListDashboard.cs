using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.IT
{
    
    public class ListofDashboard
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

    public class GetDashboardLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetDashboardList> data { get; set; }
    }

    public class GetDashboardList
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