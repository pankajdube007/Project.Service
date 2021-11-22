using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListofCategorywiseSaleManagementChild
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime CurFromDate { get; set; }

        [Required]
        public DateTime CurToDate { get; set; }


        [Required]
        public DateTime LastFromDate { get; set; }

        [Required]
        public DateTime LastToDate { get; set; }

        [Required]
        public int categoryid { get; set; }
    }

    public class CategorywiseSaleManagementChilds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CategorywiseSaleManagementChild> data { get; set; }
    }

    public class CategorywiseSaleManagementChild
    {
        public int slno { get; set; }
        public string branchnm { get; set; }
        public string currentyearsaleamt { get; set; }
        public string lastyearssaleamt { get; set; }
    }
}