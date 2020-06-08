using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofComboTotalQty
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ComboId { get; set; }

        [Required]
        public string ComboQty { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ComboTotalQtys
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ComboTotalQtyFinal> data { get; set; }
    }

    public class ComboTotalQtyFinal
    {
        public List<ComboTotalQty> ComboTotalQtyDetails { get; set; }
        public string totalqty { get; set; }
    }

    public class ComboTotalQty
    {
        public string itemname { get; set; }
        public string range { get; set; }
        public string qty { get; set; }
    }
}