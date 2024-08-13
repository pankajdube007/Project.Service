using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class GiftPointSchemNameList
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }
    }
    public class GiftPointSchemNames
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GiftPointSchemName> data { get; set; }
    }
    public class GiftPointSchemName
    {
        public string Slno { get; set; }
        public string SchemeName { get; set; }
        public string IsGiftSelected { get; set; }

        public Boolean ShowSubmitButton { get; set; }
    }
}