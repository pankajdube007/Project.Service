using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
   
    public class ListBranchwiseattdetails
    {
        
        [Required]
        public string CIN { get; set; }
        [Required]
        public string cat { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public int type { get; set; }
    }
    public class BranchwiseattdetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchwiseattdetailsList> data { get; set; }
    }
    public class BranchwiseattdetailsList
    {
        public string EmployeeCode { get; set; }
        public string Employeeslno { get; set; }
        public string EmployeeName { get; set; }
        public string Departmentname { get; set; }
        public string DesignationName { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        
        public string Branchname { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string InTime { get; set; }
        public string Out { get; set; }
        public string Dueration { get; set; }
        public string MobileNumber { get; set; }
        public string JoinDate { get; set; }
        public string WorkYear { get; set; }
        public string Fatehr { get; set; }
        public string Mother { get; set; }
        public string Email { get; set; }

    }
}