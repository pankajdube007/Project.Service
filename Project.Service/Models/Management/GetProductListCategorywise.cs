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
        [Required]
        public int Usercat { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Productname { get; set; }
        [Required]
        public int ProductCat { get; set; }
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
        public string SlNo { get; set; }
        public string Name { get; set; }
        public string Points { get; set; }
        public string ProductCategoryId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string ProductSubCategoryID { get; set; }
        public string ShortDescription { get; set; }
        public string ProductPic { get; set; }
        public string TotalCount { get; set; }
    }
}