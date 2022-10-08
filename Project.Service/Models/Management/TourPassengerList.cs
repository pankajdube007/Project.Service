using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListofTourPassenger
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

    }

    public class TourPassengerLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TourPassengerList> data { get; set; }
    }

    public class TourPassengerList
    {
        public string SlNo { get; set; }
        public string PassengerName { get; set; }
    }
}