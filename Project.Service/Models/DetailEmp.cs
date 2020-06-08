using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  
    public class ListDetailEmp
    {
        [Required]
        public int slno { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class DetailEmpLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DetailEmpList> data { get; set; }
    }
    public class DetailEmpList
    {

        public string Name { get; set; }
        public string JoinDate { get; set; }
        public string DOB { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string Sublocation { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string ContactNo { get; set; }
        public string WorkExp { get; set; }
        public string img { get; set; }
        public string CTC { get; set; }
        public string Branch { get; set; }
        public string Email { get; set; }
        public string address { get; set; }

        public string empcode { get; set; }
    }
}