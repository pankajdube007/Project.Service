using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   

    public class NukkadMeetSMSList
    {
        [Required]
        public int ExecId { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class NukkadMeetSMSS
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NukkadMeetSMS> data { get; set; }
    }


    public class NukkadMeetSMS
    {
        public string Sent { get; set; }

       


    }

}