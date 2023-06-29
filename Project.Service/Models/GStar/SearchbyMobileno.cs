using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   
    public class SearchbyMobilenotList
    {
        [Required]
        public int ExecId { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class SearchbyMobilenoS
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SearchbyMobileno> data { get; set; }
    }


    public class SearchbyMobileno
    {
        public string Slno { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Category { get; set; }

        public string Status { get; set; }

        public string Address { get; set; }

        public string Pincode { get; set; }

        public string Photo { get; set; }


    }

}