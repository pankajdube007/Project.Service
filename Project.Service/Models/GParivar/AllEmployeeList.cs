using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  

    public class ListAllEmployeeList
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class EmployeeAllLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<EmployeeAllList> data { get; set; }
    }
    public class EmployeeAllList
    {
        public int slno { get; set; }
        public string Name { get; set; }
    }
}