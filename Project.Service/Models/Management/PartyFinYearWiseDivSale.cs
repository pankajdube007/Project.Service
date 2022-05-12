using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofPartyFinYearWiseDivSale
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string FinYear { get; set; }

    }

    public class PartyFinYearWiseDivSales
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PartyFinYearWiseDivSale> data { get; set; }
    }

    public class PartyFinYearWiseDivSale
    {
        public string DivId { get; set; }
        public string DivName { get; set; }
        public string MonthName { get; set; }
        public string Sale { get; set; }
       
    }
}