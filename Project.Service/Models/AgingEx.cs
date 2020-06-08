using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofAgingEx
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
        public int count { get; set; }
    }

    public class AgingExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AgingEx> data { get; set; }
    }

    public class AgingEx
    {
        public List<AgingExDeatail> AgingDetails { get; set; }
        public List<AgingExurl> Agingurls { get; set; }
        public bool ismore { get; set; }
    }

    public class AgingExDeatail
    {
        public string partynam { get; set; }
        public string higherdays { get; set; }
        public string exnm { get; set; }
        public string zeroto30 { get; set; }
        public string thirtyoneto60 { get; set; }
        public string sixtyoneto90 { get; set; }
        public string nintyonetoabove { get; set; }
    }

    public class AgingExurl
    {
        public string url { get; set; }
        public string zeroto30total { get; set; }
        public string thirtyoneto60total { get; set; }
        public string sixtyoneto90total { get; set; }
        public string nintyonetoabovetotal { get; set; }
        public string finaltotal { get; set; }
    }
}