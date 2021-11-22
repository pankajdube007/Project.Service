using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{


    public class ListLocationwiseAttCount
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
    public class LocationwiseAttCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LocationwiseAttCountList> data { get; set; }
    }
    public class LocationwiseAttCountList
    {
        public string LocationName { get; set; }
        public int LocationId { get; set; }
        public string SalesexecPresent { get; set; }
        public string SalesexecAbsent { get; set; }
        public string EmployeePresent { get; set; }
        public string EmployeeAbsent { get; set; }
    }
}