using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class testExecutivePresentAbsent
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class testExecutivePresentAbsentLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<testExecutivePresentAbsentList> data { get; set; }
    }

    public class testExecutivePresentAbsentList
    {
        public string Attendance { get; set; }
        public string AttendanceStatus { get; set; }
        public string datetime { get; set; }
        public string lat { get; set; }
        public string Long { get; set; }
        public string Address { get; set; }
        public int islock { get; set; }

        public string orgname { get; set; }
        public string orgid { get; set; }
        public string catid { get; set; }
        public string  catname { get; set; }
        public int checkinoutstatus { get; set; }

    }
}