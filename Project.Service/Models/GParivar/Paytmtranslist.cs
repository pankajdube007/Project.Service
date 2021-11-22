using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class Paytmtranslist
    {
        [Required]
        public string ClientSecret { get; set; }
    }
    public class Paytmtranss
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Paytmtrans> data { get; set; }
    }

    public class Paytmtrans
    {
        public string mobile { get; set; }
        public decimal amount { get; set; }
    }
}