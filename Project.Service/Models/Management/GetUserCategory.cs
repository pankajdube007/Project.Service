using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ListGetUserCategory
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }
    }
    public class GetUserCategorys
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetUserCategory> data { get; set; }
    }
    public class GetUserCategory
    {
        public int SLno { get; set; }
        public string CategoryName { get; set; }
    }
}