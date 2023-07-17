using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GetListofAutomationLeadGenerationArchitectNo
    {
        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }


    public class GetListAutomationLeadGenerationArchitectNos
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListAutomationLeadGenerationArchitectNo> data { get; set; }
    }

    public class GetListAutomationLeadGenerationArchitectNo
    {
        public string slno { get; set; }
        public string Architech_No { get; set; }
        public string Architech_Name { get; set; }
        public string compname { get; set; }
        
    }
}