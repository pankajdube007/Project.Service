using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class Branchwisecategorywisestockvalueandageingmanagement
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int BranchID { get; set; }
       
        [Required]
        public int DivID { get; set; }
    }
    public class BranchwisecategorywisestockvalueandageingmanagementLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BranchwisecategorywisestockvalueandageingmanagementList> data { get; set; }
    }

    public class BranchwisecategorywisestockvalueandageingmanagementList
    {
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Stock { get; set; }

        public string Stockvale { get; set; }

        public string to2monthstock { get; set; }

        public string to2monthvalue { get; set; }

        public string to6monthstock { get; set; }

        public string to6monthvalue { get; set; }
        public string to12monthstock { get; set; }
        public string to12monthvalue { get; set; }
        public string to1to2yearstock { get; set; }
        public string to1to2yearstockvalue { get; set; }
        public string morethan2yearstock { get; set; }
        public string morethan2yearstockvalue { get; set; }

    }
}