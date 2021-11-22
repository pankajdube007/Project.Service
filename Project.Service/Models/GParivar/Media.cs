using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListOfMedia
    {
        [Required]
        public int CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class MediaLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MediaList> data { get; set; }
    }

    public class MediaList
    {
        public string VideoLink { get; set; }
        public string Subject { get; set; }

        public string MediaType { get; set; }
        public string Details { get; set; }
        public string Images { get; set; }
    }
}