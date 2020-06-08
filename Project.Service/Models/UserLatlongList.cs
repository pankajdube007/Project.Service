using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class UserLatlongListAction
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please input a valid stateid")]
        public int stateid { get; set; }

        [Required]
        public string username { get; set; }

        [DateRange]
        public DateTime asondate { get; set; }

        [Required]
        public string uniquekey { get; set; }
    }

    public class listlatlongs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<listlatlong> data { get; set; }
    }

    public class listlatlong
    {
        //public int slno { get; set; }
        public string username { get; set; }

        public int userid { get; set; }
        public int stateid { get; set; }
        public List<positionlatlong> position { get; set; }
    }

    public class positionlatlong
    {
        public int orderno { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public DateTime latlngtmdt { get; set; }
        public string timestamp { get; set; }
        public string duration { get; set; }
        public string place { get; set; }
        public string address { get; set; }
    }
}