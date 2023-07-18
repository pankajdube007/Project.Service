using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   

    public class GetListofAutomationLeadGenerationCategory
    {

        //[Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        //[Required]
        public string CIN { get; set; }
    }



    public class GetListAutomationLeadGenerationCategorys
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListAutomationLeadGenerationCategory> data { get; set; }
    }

    public class GetListAutomationLeadGenerationCategory
    {
        public string SlNo { get; set; }
        public string categorynm { get; set; }
        
    }
}