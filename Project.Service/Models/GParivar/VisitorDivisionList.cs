using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{
   
    public class ListDivisionVisitor
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetDivisionVisitors
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetDivisionVisitor> data { get; set; }
    }

    public class GetDivisionVisitor
    {
        public int SlNo { get; set; }
        public string DivisionName { get; set; }
        

    }
}