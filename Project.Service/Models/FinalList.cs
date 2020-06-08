using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListFinalList
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class FinalLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FinalList> data { get; set; }
    }

    public class FinalList
    {
        public int teamid { get; set; }
        public string team { get; set; }
        public int partyid { get; set; }
        public int typecat { get; set; }
    }
}