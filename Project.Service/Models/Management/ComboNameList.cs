using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ComboNameListe
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
    }
    public class ComboNameLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ComboNameList> data { get; set; }
    }
    public class ComboNameList
    {
        public string Slno { get; set; }
        public string ComboName { get; set; }
    }
}