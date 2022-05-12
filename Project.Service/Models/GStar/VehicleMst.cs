using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   


    public class ListAddVehicleMst
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int VehicleType { get; set; }

        [Required]
        public string VehicleNo { get; set; }

        [Required]
        public int OwnedBy { get; set; }

        [Required]
        public string img { get; set; }

        public string model { get; set; }

        public string mfgby { get; set; }

       
    }

    public class AddVehicleMsts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddVehicleMst> data { get; set; }
    }

    public class AddVehicleMst
    {
        public string output { get; set; }
    }


}