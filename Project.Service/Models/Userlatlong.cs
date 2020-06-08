using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class UserlatlongAction
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid stateid")]
        public int stateid { get; set; }

        [Required]
        public string uniquekey { get; set; }
    }

    public class ulatlongs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ulatlong> data { get; set; }
    }

    public class ulatlong
    {
        //public int slno { get; set; }
        public string lat { get; set; }

        public string longi { get; set; }
        public DateTime latlongtmdt { get; set; }
        public string name { get; set; }
        public string statenm { get; set; }
    }
}