using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofLogToServer
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string NotificationId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class LogToServers
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LogToServer> data { get; set; }
    }

    public class LogToServer
    {
        public string outresult { get; set; }
    }
}