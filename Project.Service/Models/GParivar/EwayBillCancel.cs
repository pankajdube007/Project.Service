using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofEwayBillCancel
    {
        [Required] public int userid { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slno { get; set; }
        [Required] public string ewaybillno { get; set; }
        [Required] public int type { get; set; }
    }

    public class EwayBillCancel
    {
        public string ewbNo { get; set; }
        public string cancelRsnCode { get; set; }
        public string cancelRmrk { get; set; }
    }
}