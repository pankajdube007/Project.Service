using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class GetListofAutomationLeadGeneration
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Project_name { get; set; }

        //public string FromDate { get; set; }

        //public string ToDate { get; set; }
    }

    public class GetAutomationLeadGenerationLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetAutomationLeadGenerationList> data { get; set; }
    }

    public class GetAutomationLeadGenerationList
    {
        public string SlNo { get; set; }
        
        public string Reference_NO { get; set; }
        public string Cust_Name { get; set; }
        public string Cust_Mob_No { get; set; }
        
        public string FullAddress { get; set; }
        
    }
}