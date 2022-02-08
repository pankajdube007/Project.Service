using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class PresentActionCopy
    {

        [Range(1, int.MaxValue, ErrorMessage = "Please Input User ID")]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public bool Present { get; set; }

        public string Remark { get; set; }
        public string IP { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public string DeviceId { get; set; }

        public string time { get; set; }

        public int IsTimeMismatch { get; set; }

        public decimal Distance { get; set; }


        public string Address { get; set; }

    }
}