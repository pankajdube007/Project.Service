using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{
    

    public class ListofPartyWiseCombo
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string BranchId { get; set; }

    }

    public class PartyWiseCombos
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyWiseCombo> data { get; set; }
    }

    public class PartyWiseCombo
    {
        public string Party { get; set; }
        public string Count { get; set; }
        public string used { get; set; }

    }
}