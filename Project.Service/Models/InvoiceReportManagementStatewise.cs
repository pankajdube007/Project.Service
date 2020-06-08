using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class InvoiceReportManagementStatewise
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

            [Required]
            public string fromdate { get; set; }

            [Required]
            public string todate { get; set; }
        }
        public class OutputResponse
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public List<InvoiceReportManagementStatewises> data { get; set; }
        }
        public class InvoiceReportManagementStatewises
        {
            public string statenm { get; set; }
            public decimal saleamt { get; set; }

        }
    }
}