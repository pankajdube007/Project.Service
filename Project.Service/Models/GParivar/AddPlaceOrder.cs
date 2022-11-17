using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    
    public class ListofAddPlaceOrder
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

    public class AddPlaceOrders
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddPlaceOrder> data { get; set; }
    }

    public class AddPlaceOrder
    {
        public string type { get; set; }
        public string message { get; set; }
    }
}