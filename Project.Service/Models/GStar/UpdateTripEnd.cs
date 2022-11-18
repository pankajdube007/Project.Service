using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListofUpdateTripEnd
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int slno { get; set; }

        [Required]
        public string EndTripImg { get; set; }

        [Required]
        public string ToKm { get; set; }


    }

    public class UpdateTripEndLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<UpdateTripEndList> data { get; set; }
    }

    public class UpdateTripEndList
    {
        public string output { get; set; }
    }

}