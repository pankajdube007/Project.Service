using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{


    public class ListUserdistrictwisecount
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [Required]
        public string Districtid { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class UserdistrictwisecountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<UserdistrictwisecountList> data { get; set; }
    }
    public class UserdistrictwisecountList
    {
        public string DistrictName { get; set; }

        public int AprRetailer { get; set; }
        public int AprElectrician { get; set; }
        public int AprCounterBoy { get; set; }



        public int PenRetailer { get; set; }
        public int PenElectrician { get; set; }
        public int PenCounterBoy { get; set; }



        public int RejRetailer { get; set; }
        public int RejElectrician { get; set; }
        public int RejCounterBoy { get; set; }

    }
}