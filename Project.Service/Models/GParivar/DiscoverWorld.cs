using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class DiscoverWorldAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FinYear { get; set; }
    }

    public class DiscoverWorlds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DiscoverWorld> data { get; set; }
    }

    public class DiscoverWorld
    {
        public List<DiscoverWorldDetails> Details { get; set; }
        public List<DiscoverWorldurls> url { get; set; }
    }

    public class DiscoverWorldDetails
    {
        public string currentamt { get; set; }
        public string lstyramt { get; set; }
        public string target { get; set; }
        public string bellowbase { get; set; }
        public string bellowamt { get; set; }
        public string salereturnamt { get; set; }
        public string points { get; set; }
        public string bonus { get; set; }
        public string growthbonus { get; set; }
        public string totalpoint { get; set; }
    }

    public class DiscoverWorldurls
    {
        public string viewinfourl { get; set; }
        public string detailsworkingurl { get; set; }
    }
}