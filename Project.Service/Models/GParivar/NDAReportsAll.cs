using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{
    public class ListsofNDAReportsAll
    {
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class NDAReportLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NDAReportList> data { get; set; }
    }

    public class NDAReportList
{
        public string Name { get; set; }
        public string cin { get; set; }
        public string SalesExecutive { get; set; }
        public string Amount { get; set; }
        public string JoinDate { get; set; }

    }
}