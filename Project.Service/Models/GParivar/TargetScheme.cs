using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofTargetScheme
    {
       
        [Required]
        public string Cin { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class TargetSchemeLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TargetSchemeList> data { get; set; }
    }

    public class TargetSchemeList
    {
        public string Scheme { get; set; }
        public string TargetQty { get; set; }
        public string Growth { get; set; }
        public string SchemeId { get; set; }
        public string Category { get; set; }
        public string Division { get; set; }
        public string qty { get; set; }
    }
}