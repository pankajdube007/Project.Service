using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListAddsalereturnrequestitem
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Salereturnrequestid { get; set; }

        [Required]
        public string ItemId { get; set; }

        [Required]
        public string Qty { get; set; }

        [Required]
        public string actiontype { get; set; }
    }

    public class Salereturnrequestitems
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Salereturnrequestitem> data { get; set; }
    }

    public class Salereturnrequestitem
    {
        public string output { get; set; }
    }
}