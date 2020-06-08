using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListTeamImage
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class TeamImageLists
    {

        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TeamImageList> data { get; set; }
    }

    public class TeamImageList
    {
        public string EventName { get; set; }
        public string TeamName { get; set; }
        public int EventId { get; set; }
        public int TeamId { get; set; }
        public string url { get; set; }
    }
}