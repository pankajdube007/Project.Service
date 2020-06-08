using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListAnalyticsData
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string ScreenId { get; set; }

        [Required]
        public string AppId { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string DateTime { get; set; }

        [Required]
        public string ScreenName { get; set; }

        [Required]
        public string OSVersion { get; set; }

        [Required]
        public string DeviceModel { get; set; }

        [Required]
        public string DeviceType { get; set; }
    }

    public class AnalyticsDatas
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AnalyticsData> data { get; set; }
    }

    public class AnalyticsData
    {
        public string output { get; set; }
    }
}