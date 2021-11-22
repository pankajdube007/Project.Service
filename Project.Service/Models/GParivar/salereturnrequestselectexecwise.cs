using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class Listsalereturnrequestselectexecwise
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class salereturnrequestselectexecwises
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<salereturnrequestselectexecwise> data { get; set; }
    }

    public class salereturnrequestselectexecwise
    {
        public int Slno { get; set; }
        public string requestno { get; set; }
        public int qty { get; set; }
        public int divid { get; set; }
        public string divisionnm { get; set; }
        public string requestdt { get; set; }
        public int levelid { get; set; }
        public string partyname { get; set; }
        public string rtype { get; set; }
        public string qtytype { get; set; }
        public string level { get; set; }
        public string pickupdate { get; set; }
        public string pickuptime { get; set; }
        public int amount { get; set; }
        public string finalize { get; set; }
        public string finalizedate { get; set; }
    }
}