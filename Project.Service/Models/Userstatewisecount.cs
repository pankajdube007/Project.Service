using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
 

    public class ListUserstatewisecount
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
        public string Stateid { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class UserstatewisecountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<UserstatewisecountList> data { get; set; }
    }
    public class UserstatewisecountList
    {
        public string StateName { get; set; }

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