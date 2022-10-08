using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListofHotelwiseRoomCount
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

    }

    public class HotelwiseRoomCounts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<HotelwiseRoomCount> data { get; set; }
    }

    public class HotelwiseRoomCount
    {
        public string HotelID { get; set; }
        public string ProductName { get; set; }
        public string HotelName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TotalRoomCount { get; set; }
        public string TotalRoomsAdded { get; set; }
        public string TotalBookedRoom { get; set; }
        public string VacantRoom { get; set; }
        
    }
}