using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class Listlastchecindetail
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class lastchecindetail
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<lastchecindetails> data { get; set; }
    }

    public class lastchecindetails
    {
        public string Lat { get; set; }
        public string Lan { get; set; }

    }
}

