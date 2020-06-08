using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofPlaceOrder
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int DivisionId { get; set; }

        [Required]
        public object OrderDetails { get; set; }

        [Required]
        public string Amount { get; set; }


        public string DeviceType { get; set; }

        public string remark { get; set; }
    }

    public class PlaceOrders
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PlaceOrder> data { get; set; }
    }

    public class PlaceOrder
    {
        public string type { get; set; }
        public string message { get; set; }
    }
}