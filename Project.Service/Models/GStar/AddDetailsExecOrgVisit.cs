using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    

    public class AddListExecOrgVisit
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int orgid { get; set; }

        [Required]
        public int orgcat { get; set; }

        [Required]
        public int visittype { get; set; }

        [Required]
        public int purposetype { get; set; }

        [Required]
        public string remark { get; set; }


    }

    public class AddExecOrgVisitLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddExecOrgVisitList> data { get; set; }
    }

    public class AddExecOrgVisitList
    {
        public string output { get; set; }
    }
}