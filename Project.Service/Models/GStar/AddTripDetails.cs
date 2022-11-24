using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    //public class AddTripDetails
    //{
    //}

    public class ListAddTrip
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int VehicleID { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string StartTripImg { get; set; }

        [Required]
        public string FromKm { get; set; }

        
        public string EndTripImg { get; set; }

        
        public string ToKm { get; set; }

        [Required]
        public int slno { get; set; }
        
    }

    public class AddTripLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddTripList> data { get; set; }
    }

    public class AddTripList
    {
        public string output { get; set; }
    }
}