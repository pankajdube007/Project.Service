using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListVehicleList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetVehicleLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetVehicleList> data { get; set; }
    }

    public class GetVehicleList
    {
        public string VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public string img { get; set; }
        public string model { get; set; }
        public string mfgby { get; set; }
        public string VehicleType { get; set; }
        public string OwnedBy { get; set; }
    }

}