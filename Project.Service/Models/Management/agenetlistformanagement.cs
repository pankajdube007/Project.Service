using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models.Management
{
    public class Listofagenetlistformanagement
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
    }
    public class agenetlistformanagementLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<agenetlistformanagementList> data { get; set; }
    }
    public class agenetlistformanagementList
    {
        public string slno { get; set; }
        public string agentcode { get; set; }
        public string agentnm { get; set; }
        public string email { get; set; }
       
    }
}