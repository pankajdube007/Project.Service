using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class LatLongByUserAction
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please input a valid User")]
        public int userid { get; set; }

        [DateRange]
        public DateTime fromdt { get; set; }

        [DateRange]
        public DateTime todt { get; set; }

        [Required]
        public string uniquekey { get; set; }
    }

    public class latlongbyus
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<latlongbyu> data { get; set; }
    }

    public class latlongbyu
    {
        public int slno { get; set; }
        public string lat { get; set; }
        public string longi { get; set; }
        public string latlongtmdt { get; set; }
        public string name { get; set; }
        public string statenm { get; set; }
    }
}