using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofPendingOrderPDFAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string AsonDate { get; set; }

        [Required]
        public int Type { get; set; }
    }

    public class PendingOrderPDFs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PendingOrderPDF> data { get; set; }
    }

    public class PendingOrderPDF
    {
        public string url { get; set; }
    }
}