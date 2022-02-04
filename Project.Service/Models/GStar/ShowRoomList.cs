using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ShowRoomList
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        
    }

  

    public class ShowRoomAppLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ShowRoomAppList> data { get; set; }
    }

    public class ShowRoomAppList
    {
        public int Slno { get; set; }
        public string ShowRoomName { get; set; }

    }
}