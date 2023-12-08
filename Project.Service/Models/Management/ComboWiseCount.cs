using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    
    public class ListofComboWiseCount
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ComboId { get; set; }

    }

    public class ComboWiseCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ComboWiseCountList> data { get; set; }
    }

    public class ComboWiseCountList
    {
        public string ComboName { get; set; }
        public string NumberOfCombo { get; set; }
        public string used { get; set; }

        public string ComboIds { get; set; }

    }
}