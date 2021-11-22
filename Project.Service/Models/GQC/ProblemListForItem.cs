using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Models
{
    public class ProblemListForItem
    {

        [Required]
        public int itemid { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class GetProblemList
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ProblemList> data { get; set; }
    }

    public class ProblemList
    {

        public int slno { get; set; }
        public string ItemProblem { get; set; }

    }
}