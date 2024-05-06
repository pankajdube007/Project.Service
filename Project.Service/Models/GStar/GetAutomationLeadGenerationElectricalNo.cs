using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GetAutomationLeadGenerationElectricalNo
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


    public class GetListAutomationLeadGenerationElectricalNos
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListAutomationLeadGenerationElectricalNo> data { get; set; }
    }


    public class GetListAutomationLeadGenerationElectricalNo
    {
        public string slno { get; set; }
        public string ElectricalNo { get; set; }
        public string Electrical_Name { get; set; }
        public string compname { get; set; }

    }
}