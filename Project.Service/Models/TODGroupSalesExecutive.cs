using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class TODGroupSalesExecutive
    {

        public class InputRequest
        {
            [Required]
            public int ExId { get; set; }

            [Required]
            public string ClientSecret { get; set; }

        }
        public class OutputResponse
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public List<TODGroupSalesExecutives> data { get; set; }
        }
        public class TODGroupSalesExecutives
        {
            public string groupid { get; set; }
            public string groupnm { get; set; }
        }
    }
}