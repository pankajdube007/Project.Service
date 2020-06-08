using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{


    public class CategoryDivWie
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Div { get; set; }
    }

    public class CategoryDivWieLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CategoryDivWieList> data { get; set; }
    }

    public class CategoryDivWieList
    {
        public int CatID { get; set; }
        public string Cat { get; set; }
       
    }
}