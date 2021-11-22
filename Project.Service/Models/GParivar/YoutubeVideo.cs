using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofYoutubeVideoAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class YoutubeVideos
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<YoutubeVideoFinal> data { get; set; }
    }

    public class YoutubeVideoFinal
    {
        public List<YoutubeVideoEvents> events { get; set; }
        public List<YoutubeVideoAdvertisement> advertisement { get; set; }
        public List<YoutubeVideoProduct> product { get; set; }
        public List<YoutubeVideoTechnical> technical { get; set; }
    }

    public class YoutubeVideoProduct
    {
        public string videolink { get; set; }
        public string images { get; set; }
        public string subject { get; set; }
        public string details { get; set; }
        public string hour { get; set; }
        public string minute { get; set; }
        public string second { get; set; }
    }

    public class YoutubeVideoAdvertisement
    {
        public string videolink { get; set; }
        public string images { get; set; }
        public string subject { get; set; }
        public string details { get; set; }
        public string hour { get; set; }
        public string minute { get; set; }
        public string second { get; set; }
    }

    public class YoutubeVideoEvents
    {
        public string videolink { get; set; }
        public string images { get; set; }
        public string subject { get; set; }
        public string details { get; set; }
        public string hour { get; set; }
        public string minute { get; set; }
        public string second { get; set; }
    }

    public class YoutubeVideoTechnical
    {
        public string videolink { get; set; }
        public string images { get; set; }
        public string subject { get; set; }
        public string details { get; set; }
        public string hour { get; set; }
        public string minute { get; set; }
        public string second { get; set; }
    }
}