using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   

    public class ListAppDownload
    {
        
        [Required]
        public string ClientSecret { get; set; }
    }
    public class AppDownloadLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AppDownload> data { get; set; }
    }
    public class AppDownload
    {
        public string AppUrl { get; set; }      
    }



    public class ListAppUpdate
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Url { get; set; }
    }
    public class AppUpdateLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AppUpdate> data { get; set; }
    }
    public class AppUpdate
    {
        public string AppUrl { get; set; }
    }

}
