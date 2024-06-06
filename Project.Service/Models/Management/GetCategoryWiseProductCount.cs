using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ListGetCategoryWiseProductCount
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
    }
    public class GetCategoryWiseProductCounts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetCategoryWiseProductCount> data { get; set; }
    }
    public class GetCategoryWiseProductCount
    {
        public string ProductCategory { get; set; }
        public string ProductCount { get; set; }
    }
}