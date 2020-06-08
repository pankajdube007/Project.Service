using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofHeadWiseExpenseChild
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string fromdate { get; set; }

        [Required]
        public string todate { get; set; }

        [Required]
        public int branchid { get; set; }

        [Required]
        public string headnm { get; set; }


    }

    public class HeadWiseExpenseChilds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<HeadWiseExpenseChild> data { get; set; }
    }

    public class HeadWiseExpenseChild
    {

        public string partynm { get; set; }
        public string voucherno { get; set; }
        public string date { get; set; }
        public string instrumenttype { get; set; }
        public string chequeno { get; set; }
        public string chequedt { get; set; }
        public string amount { get; set; }
        public string narration { get; set; }
        public string remark { get; set; }
        public string links { get; set; }
    }
}