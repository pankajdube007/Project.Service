using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListSemiFinalList
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class SemiFinalLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SemiFinalList> data { get; set; }
    }

    public class SemiFinalList
    {
        public int teamid { get; set; }
        public string team { get; set; }
        public int partyid { get; set; }
        public int typecat { get; set; }
    }
}