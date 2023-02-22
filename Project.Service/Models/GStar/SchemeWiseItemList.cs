using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ListSchemeWiseItemList
    {
        [Required]
        public int schemeid { get; set; }

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class SchemeWiseItemList
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SchemeWiseItemLists> data { get; set; }
    }

    public class SchemeWiseItemLists
    {
        public string Itemid { get; set; }
        public string ItemName { get; set; }
        public string Divisionid { get; set; }
        public string Divisionnm { get; set; }
    }
}