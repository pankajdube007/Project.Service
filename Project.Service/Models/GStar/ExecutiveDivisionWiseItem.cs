using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class ExecutiveDivisionWiseItem
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Search { get; set; }
    }

    public class ExecutiveDivisionWiseItems
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetExecutiveDivisionWiseItems> data { get; set; }
    }

    public class GetExecutiveDivisionWiseItems
    {
        public string SLno { get; set; }

        public string Searchitem{ get; set; }

        public string Divisionid { get; set; }

        public string DivisionName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ItemCode { get; set; }

        public string ItemDescription { get; set; }
    }
}