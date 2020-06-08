using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListDivisionWiseSubCatImg
    {
        [Required]
        public string CIN { get; set; }
      
        public string Search { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class DivisionWiseSubCatImgs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DivisionWiseSubCatImg> data { get; set; }
    }

    public class DivisionWiseSubCatImg
    {
        public string Division { get; set; }
        public string SubCat { get; set; }
        public string urlimg { get; set; }
        public string urlpdf { get; set; }
    }
}