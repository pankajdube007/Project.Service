using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListOfpartycombonamewisecount
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

    }

    public class partycombonamewisecounts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<partycombonamewisecount> data { get; set; }
    }
    public class partycombonamewisecount
    {
        public string ComboName { get; set; }
        public string NumberOfCombo { get; set; }
        public string used { get; set; }

    }
}