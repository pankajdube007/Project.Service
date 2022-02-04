using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class FreepayBankDeactivation
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string UTRN { get; set; }

        [Required]
        public string Remarks { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }


  


    public class FreepayBankDeactivationH
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }


    public class FreepayBankDeactivationdata
    {

        public string banknm { get; set; }
        public string utrn { get; set; }
        public string date { get; set; }
        public string status { get; set; }     
        public string remark { get; set; }
    }

    public class FreepayBankDeactivations
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FreepayBankDeactivationdata> data { get; set; }
    }

}