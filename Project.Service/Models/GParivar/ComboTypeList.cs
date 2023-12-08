using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class ListComboTypeList
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }
    }
    public class ComboTypeList
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ComboTypeLists> data { get; set; }
    }
    public class ComboTypeLists
    {
        public string Slno { get; set; }
        public string ComboName { get; set; }
    }
}