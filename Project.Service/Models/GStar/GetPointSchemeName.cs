using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GetPointSchemeNameList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class GetPointSchemeName
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetPointSchemeNames> data { get; set; }
    }
    public class GetPointSchemeNames
    {
        public int Slno { get; set; }
        public string SchemeName { get; set; }
    }
}