using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{

    public class ListofDhanbarseQuickpayVideo
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Type { get; set; }

    }

    public class DhanbarseQuickpayVideos
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DhanbarseQuickpayVideo> data { get; set; }
    }


    public class DhanbarseQuickpayVideo
    {
        public string videolink { get; set; }
        public string images { get; set; }
        public string subject { get; set; }
        public string details { get; set; }
        public string hour { get; set; }
        public string minute { get; set; }
        public string second { get; set; }
    }


}