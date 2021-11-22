using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListoSupplierMastManagement
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }


    }

    public class SupplierMastManagements
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SupplierMastManagement> data { get; set; }
    }

    public class SupplierMastManagement
    {
        public string slno { get; set; }
        public string suppliernm { get; set; }
        public string code { get; set; }
    }
}