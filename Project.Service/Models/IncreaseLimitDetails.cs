using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class IncreaseLimitDetailsAction
    {
        [Required]
        public int userid { get; set; }

        public string searchtxt { get; set; }
    }

    public class IncreaseLimitDetails
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<IncreaseLimitDetail> data { get; set; }
    }

    public class IncreaseLimitDetail
    {
        public int slno { get; set; }
        public string partytype { get; set; }
        public string displaynm { get; set; }
        public string homebranch { get; set; }
        public string city { get; set; }
        public string increaselimit { get; set; }
        public string creatdt { get; set; }
        public string status { get; set; }
        public string uselimit { get; set; }
        public string usernm { get; set; }
    }
}