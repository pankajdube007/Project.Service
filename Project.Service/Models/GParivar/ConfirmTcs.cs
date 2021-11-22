using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofConfirmTcs
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string pan { get; set; }

        [Required]
        public bool turnover { get; set; }

        [Required]
        public string designation { get; set; }

        [Required]
        public string emailid { get; set; }

    }

    public class ConfirmTcss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ConfirmTcs> data { get; set; }
    }

    public class ConfirmTcs
    {
        public string output { get; set; }
    }

}