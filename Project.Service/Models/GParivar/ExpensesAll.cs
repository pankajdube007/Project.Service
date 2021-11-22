using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofExpensesAll
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }

        [Required]
        public int Hierarchy { get; set; }


    }

    public class ExpensesAlls
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExpensesAll> data { get; set; }
    }

    public class ExpensesAll
    {

        public int ExpenseId { get; set; }
        public string salesexnm { get; set; }
        public string orgnm { get; set; }
        public string catnm { get; set; }
        public string FromDt { get; set; }
        public string ToDt { get; set; }
        public string ExpenseAmt { get; set; }
        public int isGstInvoice { get; set; }
        public string GstInvoiceAmt { get; set; }
        public int isAdvance { get; set; }
        public string Invoicepdf { get; set; }
        public string InvoiceImage { get; set; }
        public string Status { get; set; }
        public string createdt { get; set; }
    }

}