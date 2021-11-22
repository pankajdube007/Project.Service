using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace Project.Service.Models
{
    public class ListofStateWisePoint
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

    }


    public class StateWisePoints
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<StateWisePoint> data { get; set; }
    }

    public class StateWisePoint
    {
        public int stateid { get; set; }
        public string statenm { get; set; }
        public string DRP { get; set; }
        public string RRP { get; set; }
        public string CRP { get; set; }




    }
}