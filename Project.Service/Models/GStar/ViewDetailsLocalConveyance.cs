using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{

    public class ListofViewDetailsLocalConveyance
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int slno { get; set; }

    }

    public class GetViewDetailsLocalConveyanceLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetViewDetailsLocalConveyanceList> data { get; set; }
    }

    public class GetViewDetailsLocalConveyanceList
    {
        public string slno { get; set; }
        public string execid { get; set; }
        public string trainApp { get; set; }
        public string metroApp { get; set; }
        public string rentalcarApp { get; set; }
        public string busApp { get; set; }
        public string autoApp { get; set; }
        public string tollApp { get; set; }
        public string AppRemark { get; set; }
        public string OtherApp { get; set; }

        public string Samedayapp { get; set; }
        public string Fixamtapp { get; set; }

        public string Outstationapp { get; set; }

        public string Foodapp { get; set; }
    }
}