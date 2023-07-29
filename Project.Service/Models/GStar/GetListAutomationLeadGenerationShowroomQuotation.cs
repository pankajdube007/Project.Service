using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GetListOfAutomationLeadGenerationShowroomQuotation
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        
    }

    public class GetAutomationLeadGenerationShowroomQuotationLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetAutomationLeadGenerationShowroomQuotationList> data { get; set; }
    }

    public class GetAutomationLeadGenerationShowroomQuotationList
    {
        public string slno { get; set; }
        public string ProductCode { get; set; }
        public string divisionnm { get; set; }
        public string UnitName { get; set; }
        public string Rate { get; set; }

    }
}