using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class getLastYearSalesFinYear
    {
        [Required]
        public string CIN { get; set; }
        
        [Required]
        public string ClientSecret { get; set; }

        public int ExecId { get; set; }

        [Required]
        public string FinYear {  get; set; }
    }

    public class getLastYearSalesFinYearss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<getLastYearSalesFinYears> data { get; set; }
    }

    public class getLastYearSalesFinYears
    {
        public string lstyearsale { get; set; }
        public string curyearsale { get; set; }
    }
}