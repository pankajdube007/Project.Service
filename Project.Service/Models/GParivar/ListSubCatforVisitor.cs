using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListSubCatforVisitor
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetSubCatforVisitors
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetSubCatforVisitor> data { get; set; }
    }

    public class GetSubCatforVisitor
    {
        public int SlNo { get; set; }
        public string RangeName { get; set; }
        public string SubcatImage { get; set; }

    }
}