using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class LatLongOfUserAction
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please input a valid userid")]
        public int userid { get; set; }

        [Required]
        public string uniquekey { get; set; }
    }

    public class LongOfUser
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LongOfUsers> data { get; set; }
    }

    public class LongOfUsers
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