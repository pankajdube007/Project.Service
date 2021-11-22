using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofDealerBankDetails
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class DealerBankDetailss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DealerBankDetails> data { get; set; }
    }

    public class DealerBankDetails
    {
        public string bankname { get; set; }
        public string utrn { get; set; }
        public string amount { get; set; }
    }
}