using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class SalesOrderPartyStatus
    {

        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int DivID { get; set; }
        [Required]
        public string Fromdate { get; set; }
        [Required]
        public string Todate { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ListSalesOrderPartyStatuss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ListSalesOrderPartyStatus> data { get; set; }
    }

    public class ListSalesOrderPartyStatus
    {
        public int Slno  { get; set; }
        public int PartyID { get; set; }
        public string PONO { get; set; }
        public string PoDate { get; set; }
        public string PoStatus { get; set; }
        public string Party { get; set; }
        public string Branch { get; set; }
        public string Total { get; set; }
        public string Url { get; set; }
        public string Uniquekey { get; set; }
        

    }
}