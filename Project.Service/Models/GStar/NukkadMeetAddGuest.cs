using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
  
    public class NukkadMeetAddGuestList
    {
        [Required]
        public int ExecId { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int MeetId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class NukkadMeetAddGuestS
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NukkadMeetAddGuest> data { get; set; }
    }


    public class NukkadMeetAddGuest
    {
        public string msg { get; set; }

    }
}