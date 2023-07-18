using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   
    public class GetListofAutomationLeadGenerationItem
    {

        //[Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int categoryid { get; set; }

        //[Required]
        public string CIN { get; set; }

    }



    public class GetListAutomationLeadGenerationItems
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListAutomationLeadGenerationItem> data { get; set; }
    }

    public class GetListAutomationLeadGenerationItem
    {
        public string SlNo { get; set; }
        public string itemnm { get; set; }

    }
}