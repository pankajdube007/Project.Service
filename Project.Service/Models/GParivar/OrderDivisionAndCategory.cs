using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    
    public class OrderDivisionAndCategory
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class OrderDivisionAndCategoryLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OrderDivisionAndCategoryList> data { get; set; }
    }

    public class OrderDivisionAndCategoryList
    {
        public int divisionid { get; set; }
        public string divisionnm { get; set; }
        public int catid { get; set; }
        public string catnm { get; set; }
        public string catimage { get; set; }
        public string ShowPlaceOrderButton { get; set; }

    }
}