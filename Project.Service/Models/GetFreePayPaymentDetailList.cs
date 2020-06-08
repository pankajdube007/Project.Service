using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class GetFreePayPaymentDetailList
    {
        [Required] public int CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slno { get; set; }
    }

    public class GetFreePayPaymentDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetFreePayPaymentDetail> data { get; set; }
    }

    public class GetFreePayPaymentDetail
    {
        public decimal savedamt { get; set; }
        public decimal totalamt { get; set; }
        public decimal adjustedamt { get; set; }
    }
}