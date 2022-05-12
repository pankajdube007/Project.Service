using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class PresentAction
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please Input User ID")]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public bool Present { get; set; }

        public string Remark { get; set; }
        public string IP { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public string DeviceId { get; set; }

        public string time { get; set; }

   
        public int IsTimeMismatch { get; set; }

        public string img { get; set; }
    }
}