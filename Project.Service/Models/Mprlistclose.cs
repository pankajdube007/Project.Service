using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListMprlistclose
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }

    }


    public class Mprlistcloses
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Mprlistclose> data { get; set; }
    }

    public class Mprlistclose
    {
        public int MPRid { get; set; }
        public string MPRRequestNO { get; set; }
        public string Requestedby { get; set; }
        public string RequestedDesignation { get; set; }
        public string RequestedDate { get; set; }
        public string RequiredDate { get; set; }
        public string PositionTitle { get; set; }
        public string NoPosition { get; set; }
        public string Budget { get; set; }
        public string EmployeeType { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string NatureOfRequest { get; set; }
        public string AgeRange { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public string PreviousEmployeeName { get; set; }
        public string PreviousEmployeeDesignation { get; set; }
        public string EducationRequirement { get; set; }
        public string PreferedQualificationExprienece { get; set; }
        public string ReplacementReason { get; set; }
        public string Department { get; set; }
        public string SubDaeprtment { get; set; }

    }
}