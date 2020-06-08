using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofCustomerReciptsExAction
    {
        [Required]
        public int ExId { get; set; }

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

        [Required]
        public int Hierarchy { get; set; }
    }

    public class CustomerReciptsExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }

        public List<CustomerReciptsExFinal> data { get; set; }
    }

    public class CustomerReciptsExFinal
    {
        public List<CustomerReciptsEx> custrecieptdata { get; set; }
        public bool ismore { get; set; }
    }

    public class CustomerReciptsEx
    {
        public int slno { get; set; }
        public string partynm { get; set; }
        public string exnm { get; set; }
        public string InstrumentType { get; set; }
        public string ChequeNo { get; set; }
        public string Reciept { get; set; }
        public string Date { get; set; }
        public string Division { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
    }
}