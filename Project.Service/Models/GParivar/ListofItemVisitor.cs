using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofItemVisitor
    {


        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string RangeID { get; set; }
    }

    public class GetListofItemVisitors
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListofItemVisitor> data { get; set; }
    }

    public class GetListofItemVisitor
    {
        public int SlNo { get; set; }
        public string ProductCode { get; set; }
        public decimal MRP { get; set; }

    }
}