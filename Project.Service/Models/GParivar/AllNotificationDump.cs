using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class AllNotificationDumpAction
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please Input User ID")]
        public int userid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Input Total no of msg You need")]
        public int count { get; set; }

        [Required]
        public string slno { get; set; }

        [Range(1, 2, ErrorMessage = "Please Input right falg")]
        public int flag { get; set; }

        [Required]
        public string uniquekey { get; set; }
    }

    public class Notifics
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Notific> data { get; set; }
    }

    public class Notific
    {
        public int slno { get; set; }
        public string msg { get; set; }
        public string sendingstamp { get; set; }
        public bool read { get; set; }
    }
}