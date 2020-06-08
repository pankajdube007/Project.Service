using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
 
    public class ListofExpenseDCR
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int DcrId { get; set; }

        [Required]
        public int ExpenseType { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string ExpenseAmt { get; set; }

        [Required]
        public int isGstInvoice { get; set; }

        [Required]
        public string GstInvoiceAmt { get; set; }

  
        public object InvoiceImage { get; set; }

        public int ExpenceId { get; set; }

        [Required]
        public string Statement { get; set; }
    }

    public class ExpenseDCRs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public object data { get; set; }
    }

    public class ExpenseDCRImage
    {

        public string img { get; set; }
        public string extension { get; set; }
    }
}