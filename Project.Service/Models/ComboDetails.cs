using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofComboDetails
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public int ComboId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ComboDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<ComboDetails> data { get; set; }
    }

    public class ComboDetails
    {
        public string itemname { get; set; }
        public string qty { get; set; }
        public string dlp { get; set; }
        public string amount { get; set; }
        public string remarks { get; set; }
    }
}