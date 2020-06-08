using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofBranshWiseNonMovementStockDetails
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int branchid { get; set; }

        [Required]
        public int type { get; set; }

    }

    public class BranshWiseNonMovementStockDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranshWiseNonMovementStockDetails> data { get; set; }
    }

    public class BranshWiseNonMovementStockDetails
    {
        public string itmecode { get; set; }
        public string itemnm { get; set; }
        public string colornm { get; set; }
        public string rangenm { get; set; }
        public string categorynm { get; set; }
        public string productcode { get; set; }
        public string divisionnm { get; set; }
        public string stockamt { get; set; }
        public string stockqty { get; set; }
        public string date { get; set; }
    }
}