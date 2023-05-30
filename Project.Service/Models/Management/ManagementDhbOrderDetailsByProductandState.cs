using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ManagementDhbOrderDetailsByProductandState
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Fromdate { get; set; }

        [Required]
        public string Todate { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public int ProductId { get; set; }


    }

    public class DataManagementDhbOrderDetailsByProductandStates
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DataManagementDhbOrderDetailsByProductandState> data { get; set; }
    }

    public class DataManagementDhbOrderDetailsByProductandState
    {
        public string ProductName { get; set; }

        public string OrderNumber { get; set; }

        public string ShopName { get; set; }

        public string CIN { get; set; }

        public string Orderstatus { get; set; }

        public string CustomerName { get; set; }

        public string MobileNo { get; set; }

        public string Workstate { get; set; }

    }
}