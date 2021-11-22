using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofActiveSchemeAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int Count { get; set; }
    }

    public class ActiveSchemes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<ActiveSchemeFinal> data { get; set; }
    }

    public class ActiveSchemeFinal
    {
        public List<ActiveScheme> activeschemedata { get; set; }
        public bool ismore { get; set; }
    }

    public class ActiveScheme
    {
        public string SchemeType { get; set; }
        public string SchemeName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Link { get; set; }
        public string imgurl { get; set; }
    }
}