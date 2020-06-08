using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class Listsalereturnrequestheadadd
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int rtype { get; set; }
        [Required] public string divid { get; set; }
        [Required] public int qty { get; set; }
        [Required] public int qtytype { get; set; }
        [Required] public int reason { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public bool imgchange1 { get; set; }
        public bool imgchange2 { get; set; }
        public bool imgchange3 { get; set; }

        public string remark { get; set; }
        [Required] public string requestpickupfromdt { get; set; }
        [Required] public string requestpickuptodt { get; set; }
    }

    public class salereturnrequestheadadds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<salereturnrequestheadadd> data { get; set; }
    }

    public class salereturnrequestheadadd
    {
        public string output { get; set; }
    }
}