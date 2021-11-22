using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofPendingOrderAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Division { get; set; }

        public string SearchText { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public string AsonDate { get; set; }

        [Required]
        public int Type { get; set; }
    }

    public class PendingOrders
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PendingOrder> data { get; set; }
    }

    public class PendingOrder
    {
        public List<PendingOrder1> pendingdata { get; set; }
        public bool ismore { get; set; }
    }

    public class PendingOrder1
    {
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Colornm { get; set; }
        public string PoNum { get; set; }
        public string PoDt { get; set; }
        public string PendingQty { get; set; }
        public string Amount { get; set; }
    }
}