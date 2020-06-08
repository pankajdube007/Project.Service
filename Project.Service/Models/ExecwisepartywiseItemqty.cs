using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Service.Models
{
    public class ListsofExecWisePartywiseqty
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string Fromdate { get; set; }

        [Required]
        public string Todate { get; set; }

        [Required]
        public string ClientSecret { get; set; }


    }

    public class ExecWisePartywiseqtys
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecWisePartywiseqty> data { get; set; }
    }

    public class ExecWisePartywiseqty
    {
        public string Party { get; set; }
        public decimal Qty { get; set; }
     
    }
}