using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class LeadTypeItem
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }


    public class LeadTypeLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LeadTypeList> data { get; set; }
    }

    public class LeadTypeList
    {
        public int Slno { get; set; }
        public string LeadType { get; set; }


    }
}