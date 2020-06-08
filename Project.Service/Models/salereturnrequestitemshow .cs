using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class Listsalereturnrequestitemshow
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int salereturnrequestid { get; set; }
    }

    public class salereturnrequestitemshows
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<salereturnrequestitemshow> data { get; set; }
    }

    public class salereturnrequestitemshow
    {
        public string itemnmnm { get; set; }
        public decimal qty { get; set; }
        public int slno { get; set; }
    }
}