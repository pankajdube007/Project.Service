using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofLastPaymentEx
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int Count { get; set; }
    }

    public class LastPaymentExs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<LastPaymentExFinal> data { get; set; }
    }

    public class LastPaymentExFinal
    {
        public List<LastPaymentEx> LastPaymentEx { get; set; }
        public bool ismore { get; set; }
    }

    public class LastPaymentEx
    {
        public string slno { get; set; }
        public string partynm { get; set; }
        public string exnm { get; set; }
        public string date { get; set; }
        public string instrumenttype { get; set; }
        public string chequeno { get; set; }
        public string amount { get; set; }
        public string status { get; set; }
    }
}