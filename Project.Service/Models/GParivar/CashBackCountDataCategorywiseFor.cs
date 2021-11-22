using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
   

    public class ListCashBackCountDataCategorywiseFordetails
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Stateid { get; set; }
        //[Required]
        //public string Categoryid { get; set; }
        [Required]
        public int   StatusType { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class CashBackCountDataCategorywiseForLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CashBackCountDataCategorywiseForList> data { get; set; }
    }
    public class CashBackCountDataCategorywiseForList
    {
    
        public string SateId { get; set; }
        public string SateName { get; set; }
        public string Retailer { get; set; }
        public string CounterBoy { get; set; }
        public string Electrician { get; set; }



    }
}