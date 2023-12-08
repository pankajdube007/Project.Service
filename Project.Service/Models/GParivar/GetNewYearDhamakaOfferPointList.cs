using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class GetNewYearDhamakaOfferPointList
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }
    }
    public class GetNewYearDhamakaOfferPointListe
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetNewYearDhamakaOfferPointLists> data { get; set; }
    }
    public class GetNewYearDhamakaOfferPointLists
    {
        public string ItemName { get; set; }
        public string InvoiceNo { get; set; }
        public string Quantity { get; set; }
        public string Point { get; set; }
    }
}