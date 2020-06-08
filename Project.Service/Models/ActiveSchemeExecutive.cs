﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
 

    public class ListsofActiveSchemeExecutiveAction
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int Count { get; set; }
    }

    public class ActiveSchemeExecutives
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<ActiveSchemeExecutiveFinal> data { get; set; }
    }

    public class ActiveSchemeExecutiveFinal
    {
        public List<ActiveSchemeExecutive> activeschemedata { get; set; }
        public bool ismore { get; set; }
    }

    public class ActiveSchemeExecutive
    {
        public string SchemeType { get; set; }
        public string SchemeName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Link { get; set; }
        public string imgurl { get; set; }
    }
}