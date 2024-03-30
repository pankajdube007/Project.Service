using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListGetallpaymenttypelist
    {
        [Required]
        public int CIN { get; set; }

        [Required]
        public string sdate { get; set; }

        [Required]
        public string edate { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]
        public int Count { get; set; }

       
        public int ExecId { get; set; }
        public string Type { get; set; }

    }

    public class Getallpaymenttypelists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetallpaymenttypelistFinal> data { get; set; }
    }

    public class Getallpaymenttypelist
    {
        public string invoiceno { get; set; }
        public string date { get; set; }
        public decimal amount { get; set; }
        public string doctype { get; set; }
        public string url { get; set; }
    }

    public class GetallpaymenttypelistFinal
    {
        public List<Getallpaymenttypelist> dispatchdata { get; set; }
        public bool ismore { get; set; }
    }
}