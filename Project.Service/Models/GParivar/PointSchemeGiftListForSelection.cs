using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class PointSchemeGiftListForSelection
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]

        public int SchemeID { get; set; }
    }

    public class PointSchemeGiftListForSelectionLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PointSchemeGiftListForSelectionList> data { get; set; }
    }

    public class PointSchemeGiftListForSelectionList
    {
        public string Slno { get; set; }
        public string GroupID { get; set; }
        public string Points { get; set; }
        public string Gift { get; set; }
        public string Address { get; set; }
        public string GiftImg { get; set; }
        public string CNValue { get; set; }
        public string IsSelected { get; set; }
        public string SelectedQty { get; set; }

    }
}