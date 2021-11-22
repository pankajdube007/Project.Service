using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofLoyaltyction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class Loyaltys
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<Loyalty> data { get; set; }
    }

    public class Loyalty
    {
        public string schemename { get; set; }
        public string fromdt { get; set; }
        public string todate { get; set; }
        public string url { get; set; }
        public string appurl { get; set; }
    }
}