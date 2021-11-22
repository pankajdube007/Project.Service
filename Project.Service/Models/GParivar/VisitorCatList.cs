using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListCatforVisitor
    {
        [Required]
        public int ExId { get; set; }
        [Required]
        public int Div { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetCatforVisitors
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetCatforVisitor> data { get; set; }
    }

    public class GetCatforVisitor
    {
        public int SlNo { get; set; }
        public string CatName { get; set; }
        public string CatImage { get; set; }

    }
}