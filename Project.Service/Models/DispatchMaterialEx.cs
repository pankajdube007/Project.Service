using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofDispatchMaterialEx
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }

        [Required]
        public string FromDate { get; set; }

        [Required]
        public string ToDate { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]
        public int Count { get; set; }

        public string searchtxt { get; set; }
    }

    public class DispatchMaterialExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DispatchMaterialExFinal> data { get; set; }
    }

    public class DispatchMaterialExFinal
    {
        public List<DispatchMaterialEx> dispatchdata { get; set; }
        public bool ismore { get; set; }
    }

    public class DispatchMaterialEx
    {
        public string PartyName { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Division { get; set; }
        public string Amount { get; set; }
        public string LrNo { get; set; }
        public string TransporterName { get; set; }
        public string url { get; set; }
    }
}