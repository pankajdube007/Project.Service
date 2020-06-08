using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class GetSaleReturnRequestList
    {
        [Required] public int CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
    }

    public class GetSaleReturnRequests
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetSaleReturnRequest> data { get; set; }
    }

    public class GetSaleReturnRequest
    {
        public int slno { get; set; }
        public string requestno { get; set; }
        public string requestdt { get; set; }
        public string rtype { get; set; }
        public string divisionnm { get; set; }
        public int qty { get; set; }
        public string qtytype { get; set; }
        public string level { get; set; }
        public string levelid { get; set; }
        public int amount { get; set; }
    }
}