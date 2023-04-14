using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{

    public class ListofPassengerCountByBranchStateAirport
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
        [Required]
        public string Type { get; set; }

    }

    public class PassengerCountByBranchStateAirports
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PassengerCountByBranchStateAirport> data { get; set; }
    }

    public class PassengerCountByBranchStateAirport
    {
        public string BranchwisePassengerCount { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string TypeId { get; set; }

    }



}