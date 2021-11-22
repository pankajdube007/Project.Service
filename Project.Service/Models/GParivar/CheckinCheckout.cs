using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofCheckinCheckoutEx
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int SlNo { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class CheckinCheckoutExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CheckinCheckoutEx> data { get; set; }
    }

    public class CheckinCheckoutEx
    {
        public string checkintime { get; set; }
        public string checkouttime { get; set; }
    }
}