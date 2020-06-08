using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class StatewiseSaleCompare
    {
        //{"ClientSecret":"ABC", "Category":"Management", "fromdate":"03/06/2019", "todate":"04/06/2019","CIN":"sa@sa.com"}


        public class InputRequest
        {
            [Required]
            public string CIN { get; set; }

            [Required]
            public string ClientSecret { get; set; }

            [Required]
            public string Category { get; set; }
           
        }
        public class OutputResponse
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public List<StatewiseSaleCompares> data { get; set; }
        }
        public class StatewiseSaleCompares
        {
            public string statenm { get; set; }
            public string stateid { get; set; }
            public string currentyearsale { get; set; }
            public string previousyearsale { get; set; }
            public string previoutwoyearsale { get; set; }

        }
    }
}