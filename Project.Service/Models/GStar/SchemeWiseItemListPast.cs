using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ListSchemeWiseItemListPast
    {
        [Required]
        public int schemeid { get; set; }

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class SchemeWiseItemListPast
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SchemeWiseItemListsPast> data { get; set; }
    }

    public class SchemeWiseItemListsPast
    {
        public string Itemid { get; set; }
        public string ItemName { get; set; }
        public string Divisionid { get; set; }
        public string Divisionnm { get; set; }
    }
}