using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   

    public class ListofCategorywiseSaleManagement
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
        public int divisionid { get; set; }
    }

    public class CategorywiseSaleManagements
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CategorywiseSaleManagement> data { get; set; }
    }

    public class CategorywiseSaleManagement
    {
        public int slno { get; set; }
        public string categorynm { get; set; }
        public string currentyearsaleamt { get; set; }
        public string lastyearssaleamt { get; set; }
    }
}