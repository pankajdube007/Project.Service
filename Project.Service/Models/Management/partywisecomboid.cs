using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.Management
{
    public class ListOfpartywisecomboid
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int partycin { get; set; }
    }

    public class partywisecomboids
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<partywisecomboid> data { get; set; }
    }
    public class partywisecomboid
    {
        public string ComboName { get; set; }
        public string NumberOfCombo { get; set; }
        public string used { get; set; }

    }
}