using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class UpdateIncreaseLimit
    {
    }

    public class UpdateIncreaseLimitAction
    {
        [Required]
        public int userid { get; set; }

        [Required]
        public string cin { get; set; }

        [Required]
        public int limitamt { get; set; }
    }

    public class UpdateIncreaseLimits
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public string data { get; set; }
    }
}