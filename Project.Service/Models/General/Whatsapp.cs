using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.General
{
  
    public class ListsofWhatsapp
    {
        [Required]
        public string mobileno { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string msg { get; set; }

        public string imgurl { get; set; }
        public string date { get; set; }

        [Required]
        public string module { get; set; }

        [Required]
        public string uid { get; set; }

    }

  

}