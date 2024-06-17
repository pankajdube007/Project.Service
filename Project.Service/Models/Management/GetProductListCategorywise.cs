using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class GetProductListCategorywiseList
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        public int UserCategoryID { get; set; }

        [Required]
        public int ProductCategoryID { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int Count { get; set; }

    }
    public class GetProductListCategorywises
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetProductListCategorywise> data { get; set; }
    }
    public class GetProductListCategorywise
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Points { get; set; }
        public string ProductCategoryId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string ProductSubCategoryID { get; set; }
        public string ShortDescription { get; set; }
        public string ProductPic { get; set; }
        public string TotalCount { get; set; }
        public string UserCategoryID { get; set; }
    }
}