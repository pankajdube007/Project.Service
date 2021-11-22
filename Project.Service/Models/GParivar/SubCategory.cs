using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class SubCategory
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int CatId { get; set; }
    }

    public class SubCategoryLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SubCategoryList> data { get; set; }
    }

    public class SubCategoryList
    {
        public int subcatid { get; set; }
        public string subcatnm { get; set; }
        public string subcatimg { get; set; }
    }
}