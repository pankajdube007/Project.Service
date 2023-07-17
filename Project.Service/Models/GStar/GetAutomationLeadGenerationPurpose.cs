using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   
    public class GetListofAutomationLeadGenerationPurpose
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class GetListAutomationLeadGenerationPurposes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListAutomationLeadGenerationPurpose> data { get; set; }
    }

    public class GetListAutomationLeadGenerationPurpose
    {
        public string SlNo { get; set; }
        public string PurposeName { get; set; }

    }
}