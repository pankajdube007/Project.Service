using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofComboCompare
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int ComboIds { get; set; }
    }

    public class ComboCompares
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<ComboComparefinal> data { get; set; }
    }

    public class ComboComparefinal
    {
        public List<ComboCompare> combocomparedetails { get; set; }
        public string allcombonm { get; set; }
    }

    public class ComboCompare
    {
        public string itemid { get; set; }
        public string combogrpname { get; set; }
        public string comboname { get; set; }
        public string itemnm { get; set; }
        public string qty { get; set; }
    }
}