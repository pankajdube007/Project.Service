using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ListGetOrderStatus
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
    }
    public class GetOrderStatuse
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetOrderStatus> data { get; set; }
    }

    public class GetOrderStatus
    {
        public int SlNo { get; set; }
        public string OrderStatus { get; set; }
    }
}