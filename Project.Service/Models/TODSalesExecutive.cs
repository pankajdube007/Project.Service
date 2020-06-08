using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class TODSalesExecutive
    {
        public class InputRequest
        {
            [Required]
            public int ExId { get; set; }

            [Required]
            public string ClientSecret { get; set; }

            [Required]
            public int groupId { get; set; }

            [Required]
            public int isTODAccepted { get; set; }


            public string CIN { get; set; }

        }
        public class OutputResponse
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public List<TODSalesExecutives> data { get; set; }
        }
        public class TODSalesExecutives
        {
            public string dealernm { get; set; }
            public string cin { get; set; }
            public string qtytarget { get; set; }
            public string qtysale { get; set; }
            public string qtyshortamt { get; set; }
            public string curmnthtarget { get; set; }
            public string curmnthsale { get; set; }
            public string curmnthshortamt { get; set; }
            public int isaccepted { get; set; }

        }

    }
}