﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListsofDisputeType
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class DisputeTypes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DisputeType> data { get; set; }
    }

   

    public class DisputeType
    {
        public int disputeid { get; set; }
        public string reason { get; set; }
    }
}