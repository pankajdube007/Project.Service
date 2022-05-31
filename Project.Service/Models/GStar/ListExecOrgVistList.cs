using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    

    public class ListExecOrgVist
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int orgid { get; set; }

        [Required]
        public int orgcat { get; set; }

    }

    public class GetExecOrgVistLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetExecOrgVistList> data { get; set; }
    }

    public class GetExecOrgVistList
    {
        public string execid { get; set; }
        public string orgid { get; set; }
        public string orgcat { get; set; }
        public string visitortype { get; set; }
        public string purposetype { get; set; }
        public string visitdate { get; set; }
        public string daydiff { get; set; }
        public string remark { get; set; }
    }
}