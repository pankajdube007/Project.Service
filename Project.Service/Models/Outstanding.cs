using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofOutstandingAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Division { get; set; }
    }

    public class Outstandings
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<Outstanding> data { get; set; }
    }

    public class Outstanding
    {
        public string Due { get; set; }
        public string OverDue { get; set; }
        public string Outstandings { get; set; }
        public bool onlinepayment { get; set; }
        public bool isregistered { get; set; }
        public bool IsActive { get; set; }
        public string errormsg { get; set; }
        public bool duesquence { get; set; }
    }
}