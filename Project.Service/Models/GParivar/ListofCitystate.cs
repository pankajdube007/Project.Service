using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofCitystate
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }
   

    public class GetListofCitystates
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListofCitystate> data { get; set; }
    }

    public class GetListofCitystate
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
    }

}