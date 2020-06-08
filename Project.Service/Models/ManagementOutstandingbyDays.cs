using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
 
    public class ListofManagementOutstandingbyDays
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Days { get; set; }

    }

    public class ManagementOutstandingbyDayss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ManagementOutstandingbyDays> data { get; set; }
    }

    public class ManagementOutstandingbyDays
    {
        public string slno { get; set; }
        public string category { get; set; }
        public string partynm { get; set; }
        public string partystatus { get; set; }
        public string locnm { get; set; }
        public string city { get; set; }
        public string statenm { get; set; }
        public string commercial { get; set; }
        public string cin { get; set; }
        public string totaloutstanding { get; set; }
        public string countinvoice { get; set; }
        public string historydays { get; set; }
        public string totalbalance { get; set; }
        public string lastinvoicedate { get; set; }
        public string lastpaymentdate { get; set; }
        public string lastpaymentamt { get; set; }
    }
}