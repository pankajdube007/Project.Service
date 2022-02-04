using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class SalesDivisionBranchWise
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string fromdate { get; set; }

        [Required]
        public string todate { get; set; }

        [Required]
        public int DivisionID { get; set; }
    }


    public class DataDivisionwiseSales
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DataDivisionwiseSale> data { get; set; }
    }

    public class DataDivisionwiseSale
    {
        public int homebranchid { get; set; }
        public string HomeBranch { get; set; }
        public string saleamt { get; set; }
        


    }
}