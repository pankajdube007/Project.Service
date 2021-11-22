using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  
    public class ListAppPaymentAmtClick
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class AppPaymentAmtClickLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AppPaymentAmtClickList> data { get; set; }
    }
    public class AppPaymentAmtClickList
    {
        public string partyname { get; set; }
        public string Chequeamt { get; set; }
        public string locnm { get; set; }
        public string bankfrom { get; set; }

    }
}