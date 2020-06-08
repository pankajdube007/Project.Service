using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListJointDateWiseEmployeeCount
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    
    }
    public class JointDateWiseEmployeeCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<JointDateWiseEmployeeCountList> data { get; set; }
    }
    public class JointDateWiseEmployeeCountList
    {
        public int MonthCount { get; set; }
        public int YearCount { get; set; }
        public int MonthCountLeaving { get; set; }
        public int YearCountLeaving { get; set; }
        public int TotalCount { get; set; }

    }

}