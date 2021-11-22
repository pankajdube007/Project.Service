using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class Listofvendorsalependingorderpowise
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int vendorid { get; set; }
    }
    public class vendorsalependingorderpowiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<vendorsalependingorderpowiseList> data { get; set; }
    }
    public class vendorsalependingorderpowiseList
    {
     
        public string Itemslno { get; set; }
        public string ItemName { get; set; }
        public string ColorName { get; set; }
        public string Subcategory { get; set; }
        public string PendingQty { get; set; }
        public string PendingDays { get; set; }
        public string Ponum { get; set; }
        public string Podate { get; set; }
        public string OrderQty { get; set; }
        public string AprQty { get; set; }



    }
}