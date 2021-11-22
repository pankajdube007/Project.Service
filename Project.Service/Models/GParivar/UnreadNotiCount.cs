using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofUnreadNotiCount
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class UnreadNotiCounts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<UnreadNotiCount> data { get; set; }
    }

    public class UnreadNotiCount
    {
        public int count { get; set; }
    }
}