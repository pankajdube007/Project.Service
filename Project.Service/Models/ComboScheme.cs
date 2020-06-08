using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofComboScheme
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ComboSchemes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ComboSchemeFinal> data { get; set; }
    }

    public class ComboSchemeFinal
    {
        public List<ComboScheme> ComboSchemeDetails { get; set; }
        public string ComboSchemeUrl { get; set; }
        public bool ComboSchemeBooking { get; set; }
    }

    public class ComboScheme
    {
        public int slno { get; set; }
        public string combogrpname { get; set; }
        public string comboname { get; set; }
        public string qty { get; set; }
        public string amount { get; set; }
    }
}