using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
 
    public class ListsofOrderDetailExecutives
    {
        [Required]
        public int ExId { get; set; }

   
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }

   
        public string Search { get; set; }

        [Required]
        public int Hierarchy { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]
        public int Count { get; set; }

    }

    public class OrderDetailExecutives
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<OrderDetailExecutiveFinal> data { get; set; }
    }

    public class OrderDetailExecutive
    {
        public string partynm { get; set; }
        public string ponum { get; set; }
        public string podt { get; set; }
        public string potime { get; set; }
        public string amount { get; set; }
        public string logstatus { get; set; }
        public string orderurl { get; set; }
        public string orderstatus { get; set; }
    }

    public class OrderDetailExecutiveFinal
    {
        public List<OrderDetailExecutive> Orderdata { get; set; }
        public bool ismore { get; set; }
    }
    }