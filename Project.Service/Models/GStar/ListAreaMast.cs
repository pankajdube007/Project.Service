using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListAreaMastList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string stateid { get; set; }

        [Required]
        public string cityid { get; set; }
    }

    public class GetAreaMastLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetAreaMastList> data { get; set; }
    }

    public class GetAreaMastList
    {
        public string SlNo { get; set; }
        public string areanm { get; set; }
        
    }

}