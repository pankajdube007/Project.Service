using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{
 

    public class Listinspectionfileuplode
    {

        [Required]
        public int ExId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string img { get; set; }

       


    }

    public class inspectionfileuplodes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<inspectionfileuplode> data { get; set; }
    }

    public class inspectionfileuplode
    {
        public string output { get; set; }
    }



}