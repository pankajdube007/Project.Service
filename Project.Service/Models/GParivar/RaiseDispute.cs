using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListsofRaiseDispute
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }


        [Required]
        public int disputeid { get; set; }


 
        public string details { get; set; }


        [Required]
        public string transactionid { get; set; }
    }

    public class RaiseDisputes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<RaiseDispute> data { get; set; }
    }



    public class RaiseDispute
    {
        public string disputeno { get; set; }
    }
}