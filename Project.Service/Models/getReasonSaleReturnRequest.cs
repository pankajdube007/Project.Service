using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class getReasonSaleReturnRequestList
    {
        [Required]
        public int CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class getReasonSaleReturnRequests
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<getReasonSaleReturnRequest> data { get; set; }
    }

    public class getReasonSaleReturnRequest
    {
        public int slno { get; set; }
        public string reason { get; set; }
    }
}