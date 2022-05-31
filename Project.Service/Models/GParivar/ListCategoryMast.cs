using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    
    public class GetListCategoryMast
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }
    }

    public class GetCategoryMasts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetCategoryMast> data { get; set; }
    }

    public class GetCategoryMast
    {
        public string SlNo { get; set; }
        public string categorynm { get; set; }

    }
}