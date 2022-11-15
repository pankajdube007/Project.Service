using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    
    public class ListOfGetOrderCatItemDetails
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public int DivisionId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class GetOrderCatItemDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetOrderCatItemDetails> data { get; set; }
    }

    public class GetOrderCatItemDetails
    {
        public string CategoryId { get; set; }
        public string categorynm { get; set; }
        public string SubCategoryId { get; set; }
        public string Subcategorynm { get; set; }
        public string DivisionId { get; set; }
        public string divisionnm { get; set; }
        public string itemcode { get; set; }
        public string itemnm { get; set; }
        public string Subcategoryurl { get; set; }
    }
}