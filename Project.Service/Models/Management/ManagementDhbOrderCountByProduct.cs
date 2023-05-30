using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ManagementDhbOrderCountByProduct
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

    }

    public class DataManagementDhbOrderCountByProducts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DataManagementDhbOrderCountByProduct> data { get; set; }
    }

    public class DataManagementDhbOrderCountByProduct
    {
        public int Productid { get; set; }

        public int Stateid { get; set; }

        public string Productname { get; set; }

        public int Totalorder { get; set; }

        public int Approvalpending { get; set; }

        public int Delivered { get; set; }

        public int Deliveredpending { get; set; }
    }


}