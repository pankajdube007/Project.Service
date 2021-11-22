using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListEmployeeList
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class EmployeeLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<EmployeeList> data { get; set; }
    }
    public class EmployeeList
    {
        public int EmployeeCount { get; set; }
        public int InternalEmpCount { get; set; }
        public int ExecCount { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }

}