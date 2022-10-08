using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListofPassengerDetailsByMobileNo
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int PassengerID { get; set; }


    }

    public class PassengerDetailsByMobileNos
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PassengerDetailsByMobileNo> data { get; set; }
    }

    public class PassengerDetailsByMobileNo
    {
        public string SlNo { get; set; }
        public string PassengerName { get; set; }
        public string BranchName { get; set; }
        public string MobileNo { get; set; }
        public string PassengerQRCode { get; set; }
        public string PassportSizeImage { get; set; }
        public string EmailID { get; set; }
        public string GroupLeaderMobileNo { get; set; }
        public string State { get; set; }
        public string FromFlightNo { get; set; }
        public string FromDeparture { get; set; }
        public string FromArrival { get; set; }
        public string DeparturePNR { get; set; }
        public string ToFlightNo { get; set; }
        public string ToDeparture { get; set; }
        public string ToArrival { get; set; }
        public string ArrivalPNR { get; set; }
        public string HotelName { get; set; }
        public string HotelLocation { get; set; }
        public string CheckinDate { get; set; }
        public string CheckoutDate { get; set; }
        public string RoomType { get; set; }
        public string RoomNumber { get; set; }
        
    }
}