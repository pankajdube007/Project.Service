using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    
    public class Listluckydrawwinnerlist
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class luckydrawwinnerlistDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<luckydrawwinnerlistDetail> data { get; set; }
    }

    public class luckydrawwinnerlistDetail
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string State { get; set; }
        public string Gift { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }

    }
}