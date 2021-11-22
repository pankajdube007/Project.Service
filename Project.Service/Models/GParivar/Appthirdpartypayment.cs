using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Project.Service.Models
{

    public class ListofAppthirdpartypayment
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int Division { get; set; }

        [Required]
        public string Date { get; set; }

    }


    public class AppthirdpartypaymentLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AppthirdpartypaymentList> data { get; set; }
    }

    public class AppthirdpartypaymentList
    {
        public string Type { get; set; }
        public string TypeID { get; set; }
        public string SLNo { get; set; }
        public string refno { get; set; }
        public string date { get; set; }
        public string displayinvoice { get; set; }
        public string divisionnm { get; set; }
        public string descrpt { get; set; }
        public string invoiceamt { get; set; }
        public string duedays { get; set; }
        public string balanceamt { get; set; }
        public string intper { get; set; }
        public string intamt { get; set; }
        public string netpaybleamt { get; set; }


    }
}