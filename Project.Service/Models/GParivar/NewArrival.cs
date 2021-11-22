using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListOfNewArrival
    {
        [Required]
        public int CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class NewArrivalLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NewArrivaltList> data { get; set; }
    }

    public class NewArrivaltList
    {
        public string BranchName { get; set; }
        public string ItemName { get; set; }
    }
}