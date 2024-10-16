using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ExecCosting
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        public int branchid { get; set; }


    }
    public class ExecCostingLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecCostingList> data { get; set; }
    }
    public class ExecCostingList
    {
        public string SalesExName { get; set; }
        
        public int execid { get; set; }

        public string designation { get; set; }

        public string Contact { get; set; }

        public string Division { get; set; }

        public string Joindt { get; set; }

        public string BranchName { get; set; }

        public string lastyearsale_Hierarchy { get; set; }

        public string TillTillDateSale_Hierarchy { get; set; }

        public string lastYrSalesingle_Own { get; set; }
        public string TillDateSalesingle_Own { get; set; }
        public string costper { get; set; }
        public string LastUpdate  { get; set; }
        public string Costperhy { get; set; }
       

    }
}