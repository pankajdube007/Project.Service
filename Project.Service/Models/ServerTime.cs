using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    
    public class ServerTime
    {
        public bool result { get; set; }
        public string servertime { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }


    public class ListsofServerTime
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }


}