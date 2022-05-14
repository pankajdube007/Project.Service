using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListTripList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetTripLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetTripList> data { get; set; }
    }

    public class GetTripList
    {
        public string exeid { get; set; }
        public string vehid { get; set; }
        public string date { get; set; }
        public string refno { get; set; }
        public string starttripimg { get; set; }
        public string fromkm { get; set; }
        public string endtripimg { get; set; }
        public string tokm { get; set; }
        public string VehicleNo { get; set; }
        public string model { get; set; }
        public string mfgby { get; set; }
        public string VehicleType { get; set; }
        public string OwnedBy { get; set; }
    }

}