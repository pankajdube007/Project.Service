using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class PaytmTranInsert
    {
        public object type { get; set; }
        public object requestGuid { get; set; }
        public string orderId { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public object response { get; set; }
        public object metadata { get; set; }
    }
}