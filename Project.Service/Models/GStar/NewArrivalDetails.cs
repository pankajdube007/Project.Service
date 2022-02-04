using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class NewArrivalDetails
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public int Slno { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

   

    public class NewArrivalAppDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NewArrivalAppDetail> data { get; set; }
    }

    public class NewArrivalAppDetail
    {

        public int Slno { get; set; }
        public string ProductCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Images { get; set; }


    }
}