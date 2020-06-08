using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofPolicy
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class Policy
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Policys> data { get; set; }
    }

    public class Policys
    {
        public string PolicyName { get; set; }
        public string FromDate { get; set; }
        public string Todate { get; set; }
        public string PDF { get; set; }
        public string imgurl { get; set; }
    }
}