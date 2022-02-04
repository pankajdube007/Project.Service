using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class MediaInputList
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }


    }

    public class MediaAppLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MediaAppList> data { get; set; }
    }

    public class MediaAppList
    {
        public int Slno { get; set; }
        public string Subject { get; set; }

    }

}