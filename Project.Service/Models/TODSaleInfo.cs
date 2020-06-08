using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListTODSaleInfo
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class TODSaleInfos
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TODSaleInfo> data { get; set; }
    }

    public class TODSaleInfo
    {
       
        public string todgroupnm { get; set; }
        public string YearlyTargetAmt { get; set; }
        public string YearlySalesAmt { get; set; }
        public string YearlytradeSale { get; set; }        
        public string YearlyprojectSale { get; set; }       
        public string YearlyEarnedAmt { get; set; }
      
    }
}