using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{
    public class ListDesignationwiseempdetail
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public int DesigId { get; set; }
    }
    public class DesignationwiseempdetailLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DesignationwiseempdetailList> data { get; set; }
    }
    public class DesignationwiseempdetailList
    {
        public string EmployeeName { get; set; }
        public string Departmentname { get; set; }
        public string SubDepartmentname { get; set; }
        public string DesignationName { get; set; }
        public string EmployeeCode { get; set; }
        public string Branchname { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public string MobileNumber { get; set; }
        public string JoinDate { get; set; }
        public string WorkYear { get; set; }
        public int slno { get; set; }
    }
}