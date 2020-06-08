using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class LogoutAction
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please Input Usrer ID")]
        public int userid { get; set; }

        [Required]
        public string uniquekey { get; set; }
    }
}