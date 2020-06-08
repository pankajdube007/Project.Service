using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofCreditNoteDetail
    {
        [Required]
        public int SlNo { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class CreditNoteDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CreditNoteDetail> data { get; set; }
    }

    public class CreditNoteDetail
    {
        public string Invoice { get; set; }
        public string InvoiceDate { get; set; }
        public string Amount { get; set; }
        public string AdjustedAmount { get; set; }
    }
}