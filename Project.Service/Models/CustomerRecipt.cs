using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofCustomerReciptAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]
        public int Count { get; set; }

        public string SearchText { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }

    public class CustomerRecipts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<CustomerReciptFinal> data { get; set; }
    }

    public class CustomerReciptFinal
    {
        public List<CustomerRecipt> custrecieptdata { get; set; }
        public bool ismore { get; set; }
    }

    public class CustomerRecipt
    {
        public int slno { get; set; }
        public string InstrumentType { get; set; }
        public string Reciept { get; set; }
        public string Date { get; set; }
        public string Division { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
    }
}