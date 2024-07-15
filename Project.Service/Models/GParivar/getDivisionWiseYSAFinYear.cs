using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class getDivisionWiseYSAFinYear
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int divisionid { get; set; }
        public int ExecId { get; set; }

        [Required]
        public string FinYear { get; set; }
    }
    public class DivisionWiseYsasFinYear
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DivisionWiseYsaFinYear> data { get; set; }
    }

    public class DivisionWiseYsaFinYear
    {
        public List<DivisionWiseDetailsFinYear> DivisionWiseSale { get; set; }
        public List<DivisionWiseTotalFinYear> DivisionWiseSaleTotal { get; set; }
    }

    public class DivisionWiseDetailsFinYear
    {
        public string categorynm { get; set; }
        public string sale { get; set; }
        public string categoryid { get; set; }
        public string divisionid { get; set; }
    }

    public class DivisionWiseTotalFinYear
    {
        public string TotalSale { get; set; }
    }
}