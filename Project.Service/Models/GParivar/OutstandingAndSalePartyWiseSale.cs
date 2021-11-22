using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{
 
    public class ListOutstandingAndSalePartyWiseSale
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string Todate { get; set; }
        [Required]
        public string DivId { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class OutstandingAndSalePartyWiseSaleLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OutstandingAndSalePartyWiseSaleList> data { get; set; }
    }
    public class OutstandingAndSalePartyWiseSaleList
    {
        public string PartyName { get; set; }
        public string PartyId { get; set; }
        public string TypeCate { get; set; }
        public string BranchId { get; set; }
        public string MonthName { get; set; }
        public string outstandingamt { get; set; }
        public string finalamount { get; set; }

    }
}