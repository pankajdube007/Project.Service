using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofEWayBillCancelbyInvoice
    {
        [Required] public int userid { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slno { get; set; }
        [Required] public int type { get; set; }
    }

    public class EWayBillCancelbyInvoice
    {
        public string ewbNo { get; set; }
        public int cancelRsnCode { get; set; }
        public string cancelRmrk { get; set; }
    }


}