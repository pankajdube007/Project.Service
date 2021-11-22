using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class Listlocwiseattdetails
    {

        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public int type { get; set; }
    }
    public class locwiseattdetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<locwiseattdetailsList> data { get; set; }
    }
    public class locwiseattdetailsList
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
        public string Intime { get; set; }
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