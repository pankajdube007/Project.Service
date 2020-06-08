using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  


    public class ListLocationWiseEmpDetails
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public int type { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class LocationWiseEmpDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LocationWiseEmpDetailsList> data { get; set; }
    }
    public class LocationWiseEmpDetailsList
    {
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string Department { get; set; }
        public string BranchName { get; set; }
        public string MobileNumber { get; set; }
        public string JoinDate { get; set; }
        public string WorkYear { get; set; }
        public string Sublocation { get; set; }
        public int slno { get; set; }
    }
}