using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
 

    public class ListofPassengersDetailsByBranchStateAirport
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Typeid { get; set; }

    }

    public class PassengersDetailsByBranchStateAirports
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PassengersDetailsByBranchStateAirport> data { get; set; }
    }

    public class PassengersDetailsByBranchStateAirport
    {
        public string PassengerName { get; set; }
        public string RelationName { get; set; }
        public string MobileNo { get; set; }
        public string TravelIDNo { get; set; }
        public string BranchName { get; set; }
        public string CategoryName { get; set; }

    }
}