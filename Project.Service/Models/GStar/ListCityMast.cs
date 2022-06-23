using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListCityMastList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string stateid { get; set; }

    }

    public class GetCityMastLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetCityMastList> data { get; set; }
    }

    public class GetCityMastList
    {
        public string SlNo { get; set; }
        public string citynm { get; set; }

    }
}