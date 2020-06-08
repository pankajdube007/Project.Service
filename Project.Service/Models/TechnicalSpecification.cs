using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofTechnicalSpecification
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class TechnicalSpecification
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TechnicalSpecifications> data { get; set; }
    }

    public class TechnicalSpecifications
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string url { get; set; }
        public string imgurl { get; set; }
    }
}