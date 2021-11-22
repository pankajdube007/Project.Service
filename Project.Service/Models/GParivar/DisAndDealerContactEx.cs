using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofDisAndDealerContactEx
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int Count { get; set; }
    }

    public class DisAndDealerContactExs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DisAndDealerContactExFinal> data { get; set; }
    }

    public class DisAndDealerContactExFinal
    {
        public List<DisAndDealerContactEx> DisAndDealerContact { get; set; }
        public bool ismore { get; set; }
    }

    public class DisAndDealerContactEx
    {
        public string partynm { get; set; }
        public string exnm { get; set; }
        public string mobile { get; set; }
        public string emailid { get; set; }
        public string partytype { get; set; }
        public string address { get; set; }
        public string gstno { get; set; }
        public string contactperson { get; set; }
    }
}