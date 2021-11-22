using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  


    public class ListLocatioWiseEmpCount
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class LocatioWiseEmpCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LocatioWiseEmpCountList> data { get; set; }
    }
    public class LocatioWiseEmpCountList
    {
        public int EmployeeCount { get; set; }
        public int InternalEmpCount { get; set; }
        public int ExecCount { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }


}