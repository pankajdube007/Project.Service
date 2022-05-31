using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.GStar
{
    public class ListOfCity
    {

       
            [Required]
            public int ExId { get; set; }

            [Required]
            public string ClientSecret { get; set; }

        
    }


    public class getListOfCity {

        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<getListOfCityData> data { get; set; }
    }

    public class getListOfCityData {
        public int CityID { get; set; }
        public string CityName { get; set; }
        
    }
}