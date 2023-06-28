using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
 

    public class ListNukkadMeetGiustList
    {
        [Required]
        public int ExecId { get; set; }

        [Required]
        public int NukkedId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class NukkadMeetRequestGuiestByIDS
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NukkadMeetRequestGuisetByID> data { get; set; }
    }


    public class NukkadMeetRequestGuisetByID
    {
        public string Name { get; set; }

        public string Photo { get; set; }

        public string Mobile { get; set; }

        public string Category { get; set; }

        public string Status { get; set; }


    }

}