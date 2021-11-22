using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
 
    public class ListofEInvoiceCancel
    {
        [Required] public int userid { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slno { get; set; }
        [Required] public int type { get; set; }
    }

    public class EInvoiceCancel
{
        public string irn { get; set; }
        public string cnlrsn { get; set; }
        public string cnlrem { get; set; }
    }

}