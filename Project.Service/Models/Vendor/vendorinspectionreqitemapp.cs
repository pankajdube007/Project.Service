using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.Vendor
{
 


    public class ListAddvendorinspectionreqitemapp
    {

        [Required]
        public string cin { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int vendorid { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime insepectiondate { get; set; }

        [Required]
        public string itemids { get; set; }

        [Required]
        public string remark { get; set; }


    }

    public class AddvendorinspectionreqitemappLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Addvendorinspectionreqitemapp> data { get; set; }
    }

    public class Addvendorinspectionreqitemapp
    {
        public string output { get; set; }
    }

}