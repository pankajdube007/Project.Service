using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofFeedbackAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string FeedbackText { get; set; }
    }

    public class Feedbacks
    {
        public bool result { get; set; }
        public string servertime { get; set; }
        public string message { get; set; }
    }
}