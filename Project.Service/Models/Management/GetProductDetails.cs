using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class GetProductDetailsList
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int UserCategoryID { get; set; }
     
    }
    public class GetProductDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetProductDetails> data { get; set; }
    }
    public class GetProductDetails
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string ManufaturerPartNumber { get; set; }
        public string HSN { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string ProductCategoryID { get; set; }
        public string ProductSubCategoryID { get; set; }
        public string ShortDescription { get; set; }
        public string ProductPic { get; set; }
        public string Points { get; set; }
        public string UserCategoryID { get; set; }
    }
}