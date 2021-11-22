using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   

    public class ListsofEwayGeneratebyEInvoice
    {
        [Required] public int userid { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slno { get; set; }
        [Required] public int type { get; set; }
    }
    public class EwayGeneratebyEInvoices
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<EwayGeneratebyEInvoice> data { get; set; }
    }

    public class EwayGeneratebyEInvoice
    {
        public string ewaybillno { get; set; }
    }

    public class ExpShipDtls
    {
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
    }

    public class Root
    {
        public string Irn { get; set; }
        public int Distance { get; set; }
        public string TransMode { get; set; }
        public string TransId { get; set; }
        public string TransName { get; set; }
        public string TransDocDt { get; set; }
        public string TransDocNo { get; set; }
        public string VehNo { get; set; }
        public string VehType { get; set; }
        public ExpShipDtls ExpShipDtls { get; set; }
    }


}