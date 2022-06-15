using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.IT
{
    
    public class ListofCurrentWeeklyTeamDetails
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string DeviceType { get; set; }

    }

    public class GetCurrentWeeklyTeamDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetCurrentWeeklyTeamDetailsList> data { get; set; }
    }

    public class GetCurrentWeeklyTeamDetailsList
    {
        public string EmpID { get; set; }
        public string WeeklyOffDay { get; set; }
        public string MonDate { get; set; }
        public string ApprovalStatus { get; set; }
        public string FirstName { get; set; }
        public string EmployeeLastName { get; set; }
        
    }
}