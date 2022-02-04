using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
  
    public class ListAddPod
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string uniqueKey { get; set; }
        [Required]
        public string podat { get; set; }
        [Required]
        public int ispodt { get; set; }
        [Required]
        public string lat { get; set; }
        [Required]
        public string lan { get; set; }
    
        public string address { get; set; }
        [Required]
        public string Ip { get; set; }

    }

    public class AddPods
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddPod> data { get; set; }
    }

    public class AddPod
    {
        public string isResult { get; set; }
    }
}