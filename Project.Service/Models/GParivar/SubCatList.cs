using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
 

    public class ListOfSubCategory
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

      
    }

    public class SubCategoryAllLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SubCategoryAllList> data { get; set; }
    }

    public class SubCategoryAllList
    {
        public int subcatid { get; set; }
        public string subcatnm { get; set; }
        
    }
}