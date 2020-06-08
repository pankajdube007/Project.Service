using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class Executivelatlanadd
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ExecutivelatlanaddLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecutivelatlanaddList> data { get; set; }
    }

    public class ExecutivelatlanaddList
{
        public string officelat { get; set; }
        public string officelan { get; set; }
        public string officeadd { get; set; }
        public string homelat { get; set; }
        public string homelan { get; set; }
        public string homeadd { get; set; }
       
    }
}