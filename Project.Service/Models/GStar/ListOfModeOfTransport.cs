using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class ListOfModeOfTransport
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }


    }


    public class getListOfModeOfTransport
    {

        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<getListOfModeOfTransportData> data { get; set; }
    }

    public class getListOfModeOfTransportData
    {
        public int TransportId { get; set; }
        public string ModeOfTransport { get; set; }

    }
}