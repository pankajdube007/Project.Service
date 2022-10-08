using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListofHotelRoomwWisePassenger
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int HotelID { get; set; }

        [Required]
        public int Type { get; set; }

        


    }

    public class HotelRoomwWisePassengerLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<HotelRoomwWisePassengerList> data { get; set; }
    }

    public class HotelRoomwWisePassengerList
    {
        public string HotelName { get; set; }
        public string RoomType { get; set; }
        public string RoomNumber { get; set; }
        public string PassengerName { get; set; }
        public string PassengerContactNo { get; set; }

    }
}