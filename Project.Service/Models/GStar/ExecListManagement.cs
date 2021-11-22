using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{
    public class ListofExecManagement
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
    }
    public class ExecManagementLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecManagementList> data { get; set; }
    }
    public class ExecManagementList
    {
        public string slno { get; set; }
        public string name { get; set; }
       
    }
}