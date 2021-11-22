using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  
    public class ListsofHighDateForExecutive
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }

    public class HighDateForExecutiveLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<HighDateForExecutiveList> data { get; set; }
    }
    public class HighDateForExecutiveList
    {
        public string highdays { get; set; }
        public string ledgerdownload { get; set; }
        public string agingdownload { get; set; }
        public string PurchaseLedgerAmt { get; set; }
        public string SaleLedgerAmt { get; set; }
        public string Diffrence { get; set; }


    }
}