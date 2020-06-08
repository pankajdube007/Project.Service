using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListExecAttDetails
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string date { get; set; }

        [Required]
        public string ClientSecret { get; set; }


        public string Type { get; set; }
    }

    public class ExecAttDetailLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecAttDetailList> data { get; set; }
    }

    public class ExecAttDetailList
    {
        public string ExecutiveName { get; set; }
        public string Attendance { get; set; }
        public string AttendanceTime { get; set; }
        public string OutTime { get; set; }
        public string InLatitude { get; set; }
        public string InLongitude { get; set; }
        public string OutLatitude { get; set; }
        public string OutLongitude { get; set; }
    }
}