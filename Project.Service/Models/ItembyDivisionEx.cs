using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofItembyDivisionEx
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public int DivisionId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ItembyDivisionExs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItembyDivisionEx> data { get; set; }
    }

    public class ItembyDivisionEx
    {
        public int slno { get; set; }
        public string itemnmnm { get; set; }
        public string previnvoice { get; set; }
    }
}