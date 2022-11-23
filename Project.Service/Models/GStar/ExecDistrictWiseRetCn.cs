using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class ExecDistrictWiseRetCn
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }
    public class GetExecDistrictWiseRetCn
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetExecDistrictWiseRetCn1> data { get; set; }
    }

    public class GetExecDistrictWiseRetCn1
    {
 
        public string Distrctnm { get; set; }
        public string cnt { get; set; }
        public string ErpDistrictid { get; set; }
    }

}