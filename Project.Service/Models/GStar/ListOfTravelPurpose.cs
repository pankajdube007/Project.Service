using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class ListOfTravelPurpose
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }


    }


    public class getListOfTravelPurpose
    {

        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<getListOfTravelPurposeData> data { get; set; }
    }

    public class getListOfTravelPurposeData
    {
        public int PurposeId { get; set; }
        public string  PurposeName { get; set; }

    }
}