using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class LastLatLngAction
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please input a valid stateid")]
        public int stateid { get; set; }

        [DateRange]
        public DateTime asondate { get; set; }

        [Required]
        public string uniquekey { get; set; }
    }

    public class lastlatlongs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<lastlatlong> data { get; set; }
    }

    public class lastlatlong
    {
        //public int slno { get; set; }
        public string lat { get; set; }

        public string lng { get; set; }
        public DateTime latlngtmdt { get; set; }
        public string username { get; set; }
        public int userid { get; set; }
        public string times { get; set; }
        public string address { get; set; }
    }
}