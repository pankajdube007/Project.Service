using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofchequeReturnEx
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

    public class chequeReturnExs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<chequeReturnExFinal> data { get; set; }
    }

    public class chequeReturnExFinal
    {
        public List<chequeReturnEx> chequeReturnEx { get; set; }
        public bool ismore { get; set; }
    }

    public class chequeReturnEx
    {
        public string partynm { get; set; }
        public string exnm { get; set; }
        public string date { get; set; }
        public string chequeno { get; set; }
        public string amount { get; set; }
    }
}