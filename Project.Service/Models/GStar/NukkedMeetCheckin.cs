using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   

    public class NukkedMeetCheckinList
    {
        [Required]
        public int ExecId { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class NukkedMeetCheckinS
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NukkedMeetCheckin> data { get; set; }
    }


    public class NukkedMeetCheckin
    {
        public string msg { get; set; }
        
    }
}