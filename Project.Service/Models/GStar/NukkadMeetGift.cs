using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{


    public class NukkadMeetGiftList
    {
        [Required]
        public int ExecId { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int GiftID { get; set; }

        [Required]
        public int MeetID { get; set; }

        [Required]
        public string Uniquekey { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class NukkadMeetGiftS
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NukkadMeetGift1> data { get; set; }
    }


    public class NukkadMeetGift1
    {
        public string msg { get; set; }

    }
}