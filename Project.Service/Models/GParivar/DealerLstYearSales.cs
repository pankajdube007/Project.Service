using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class DealerLstYearSalesAction
    {
        [Required]
        public string CIN { get; set; }
        
        public  int ExecId { get; set; }


        [Required]
        public string ClientSecret { get; set; }
    }

    public class DealerLstYearSaless
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DealerLstYearSales> data { get; set; }
    }

    public class DealerLstYearSales
    {
        public string lstyearsale { get; set; }
        public string curyearsale { get; set; }
    }
}