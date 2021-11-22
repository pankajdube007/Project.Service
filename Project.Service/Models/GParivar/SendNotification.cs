using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofSendNotification
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class SendNotifications
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<SendNotification> data { get; set; }
    }

    public class SendNotification
    {
        public string title { get; set; }
        public string body { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public bool isread { get; set; }
        public int slno { get; set; }
        public string image { get; set; }
    }
}