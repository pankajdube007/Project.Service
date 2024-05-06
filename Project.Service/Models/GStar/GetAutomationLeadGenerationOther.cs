using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GetAutomationLeadGenerationOther
    {
        //[Required]
        public int ExId { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        //[Required]
        public string CIN { get; set; }
    }

    public class GetListAutomationLeadGenerationOthers
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListAutomationLeadGenerationOther> data { get; set; }
    }

    public class GetListAutomationLeadGenerationOther
    {
        public string slno { get; set; }
        public string OtherNo { get; set; }
        public string Other_Name { get; set; }
        public string compname { get; set; }

    }

}