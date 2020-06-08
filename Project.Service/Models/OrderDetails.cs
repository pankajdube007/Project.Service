using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofOrderDetails
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }
    }

    public class OrderDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<OrderDetails> data { get; set; }
    }

    public class OrderDetails
    {
        public string ponum { get; set; }
        public string podt { get; set; }
        public string potime { get; set; }
        public string amount { get; set; }
        public string logstatus { get; set; }
        public string orderurl { get; set; }
        public string orderstatus { get; set; }
    }
}