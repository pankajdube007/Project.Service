using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class ListofItemsByDivisionDetailsEx
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class ItemsByDivisionDetailsExs
    {
        public string result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemsByDivisionDetailsEx> data { get; set; }
    }

    public class ItemsByDivisionDetailsEx
    {
        public string invoiceno { get; set; }
        public string invoicedate { get; set; }
        public string quantity { get; set; }
    }
}