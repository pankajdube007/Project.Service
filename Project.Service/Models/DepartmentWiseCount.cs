using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{


    public class ListDepartmentWiseCount
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class DepartmentWiseCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DepartmentWiseCountList> data { get; set; }
    }
    public class DepartmentWiseCountList
    {
        public int DepartmentId { get; set; }
        public int EmpCount { get; set; }
        public string DepartmentName { get; set; }
    }
}