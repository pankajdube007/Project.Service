using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
 

    public class ListOfSubCatWiseItemList
    {
        [Required]
        public string CIN { get; set; }
        
        [Required]
        public int SubCat { get; set; }
        [Required]
        public string ClientSecret { get; set; }


    }

    public class SubCatWiseItemLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SubCatWiseItemList> data { get; set; }
    }

    public class SubCatWiseItemList
    {
        public int slno { get; set; }
        public string Item { get; set; }

    }
}