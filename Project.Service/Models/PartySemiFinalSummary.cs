using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
    public class ListPartySemiFinalSummary
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        
        [Required]
        public string TeamId { get; set; }
    }
    public class PartySemiFinalSummaryLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartySemiFinalSummaryList> data { get; set; }
    }

    public class PartySemiFinalSummaryList
    {
        public string output { get; set; }
    }
}