using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ExecutivePresentAbsent
    {
        [Required]
        public int ExId { get; set; }

        public int EmpType { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ExecutivePresentAbsentLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecutivePresentAbsentList> data { get; set; }
    }

    public class ExecutivePresentAbsentList
    {
        public string Attendance { get; set; }
        public string AttendanceStatus { get; set; }
        public string datetime { get; set; }
        //public string lat { get; set; }
        //public string Long { get; set; }
        //public string Address { get; set; }
    }
}