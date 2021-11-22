using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   
    public class ListofExpenseUnplanned
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int OrgId { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public int ExpenseType { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }

        [Required]
        public string ExpenseAmt { get; set; }

        [Required]
        public int isGstInvoice { get; set; }

        [Required]
        public string GstInvoiceAmt { get; set; }

      
        public object InvoiceImage { get; set; }

        [Required]
        public int IsAdvance { get; set; }

        public int ExpenceId { get; set; }

        [Required]
        public string Statement { get; set; }
    }

    public class ExpenseUnplanneds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public object data { get; set; }
    }

    public class ExpenseUnplannedImage
    {

        public string img { get; set; }
        public string extension { get; set; }
    }
}