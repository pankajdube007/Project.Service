using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{
 

    public class ListJointDateWiseEmployeeData
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string todate { get; set; }

        [Required]
        public string fromdate { get; set; }
        [Required]
        public int type { get; set; }

    }
    public class JointDateWiseEmployeeDataLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<JointDateWiseEmployeeDataList> data { get; set; }
    }
    public class JointDateWiseEmployeeDataList
    {
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string MobileNumber { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string Branchname { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public string JoinDate { get; set; }
        public string Leavedate { get; set; }
        public string WorkYear { get; set; }
        public int slno { get; set; }

    }
}