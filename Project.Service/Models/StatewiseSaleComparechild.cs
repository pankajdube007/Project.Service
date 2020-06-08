using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class StatewiseSaleComparechild
    {
        public class InputRequest
        {
            [Required]
            public string CIN { get; set; }

            [Required]
            public string Category { get; set; }

            [Required]
            public string ClientSecret { get; set; }

            [Required]
            public int stateid { get; set; }

        }

        public class OutputResponse
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public List<StatewiseSaleComparechilds> data { get; set; }
        }

        public class StatewiseSaleComparechilds
        {
            public string slno { get; set; }
            public string name { get; set; }
            public string cin { get; set; }
            public string partystatus { get; set; }
            public string currentyearsale { get; set; }
            public string previousyearsale { get; set; }
            public string previoustwoyearsale { get; set; }
        }
    }
}