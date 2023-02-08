using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListExecCheckInOutList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string date { get; set; }

        [Required]
        public string ClientSecret { get; set; }

       
        public string EmpType { get; set; }
    }

    public class ExecCheckInOutLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecCheckInOutList> data { get; set; }
    }

    public class ExecCheckInOutList
    {
        public string slno { get; set; }
        public string OrgId { get; set; }
        public string OrgCatId { get; set; }
        public string OrgName { get; set; }
        public string OrgCate { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public decimal Distnce { get; set; }
        public string Dcr { get; set; }
    }
}