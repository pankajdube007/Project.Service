using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{


    public class ListBranchwiseAttCount
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class BranchwiseAttCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchwiseAttCountList> data { get; set; }
    }
    public class BranchwiseAttCountList
    {
        public string BranchName { get; set; }
        public int BranchId { get; set; }
        public string SalesexecPresent { get; set; }
        public string SalesexecAbsent { get; set; }
        public string EmployeePresent { get; set; }
        public string EmployeeAbsent { get; set; }
    }


}