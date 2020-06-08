using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  

    public class ListEmployeeListDetailsData
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public int Branchid { get; set; }
        [Required]
        public int type { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class EmployeeListDetailsDataLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<EmployeeListDetailsDataList> data { get; set; }
    }
    public class EmployeeListDetailsDataList
    {
        public string EmployeeName { get; set; }
        public int slno { get; set; }
        public string EmployeeCode { get; set; }
        public string Department { get; set; }
        public string BranchName { get; set; }
        public string MobileNumber { get; set; }
        public string JoinDate { get; set; }
        public string WorkYear { get; set; }
    }


}