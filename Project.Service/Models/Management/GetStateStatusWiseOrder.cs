using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ListGetStateStatusWiseOrder
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int StatusId { get; set; }

        public int StateId { get; set; }

        [Required]
        public int UserCategoryID { get; set; }

        [Required]
        public string PivotHeader { get; set; }

        [Required]
        public string Category { get; set; }
    }
    public class GetStateStatusWiseOrders
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetStateStatusWiseOrder> data { get; set; }
    }
    public class GetStateStatusWiseOrder
    {
        public string OrderNumber { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string CatgeoryName { get; set; }
        public string OrderDate { get; set; }
        public string OrderApprovalDate { get; set; }
        public string OrderQuantity { get; set; }
        public string OrderPendingDays { get; set; }
        public string OrderStatus { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
    }
}