using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
 


    public class ListDesignationWiseCount
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }

        [Required]
        public int branchid1 { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class DesignationWiseCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DesignationWiseCountList> data { get; set; }
    }
    public class DesignationWiseCountList
    {
        public int DesignationId { get; set; }
        public int EmpCount { get; set; }
        public string DesignationName { get; set; }
    }
}