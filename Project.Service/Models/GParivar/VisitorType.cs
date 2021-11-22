using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class VisitorTypeItem
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }


    public class VisitorTypeLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<VisitorTypeList> data { get; set; }
    }

    public class VisitorTypeList
    {
        public int Slno { get; set; }
        public string VisitorType { get; set; }


    }
}