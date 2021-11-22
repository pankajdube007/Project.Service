using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Models
{
    public class RequestPartyChannelFinance 
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        

        
    }

    public class AddRequestPartyChannelFinances
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddRequestPartyChannelFinance> data { get; set; }
    }

    public class AddRequestPartyChannelFinance
    {
        public string output { get; set; }
    }
}