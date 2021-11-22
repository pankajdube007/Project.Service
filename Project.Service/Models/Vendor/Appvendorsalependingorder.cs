using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class Listofvendorsalependingorder
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
        public int cnt { get; set; }
        [Required]
        public int vendorid { get; set; }
    }
    public class vendorsalependingorderLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<vendorsalependingorderList> data { get; set; }
    }
    public class vendorsalependingorderList
    {
        //public string OrderNo { get; set; }
        //public string OrderDate { get; set; }
        //public string ItemCode { get; set; }
        //public string Division { get; set; }
        //public string Category { get; set; }
        public string Itemslno { get; set; }
        public string ItemName { get; set; }
        public string ColorName { get; set; }
        public string Subcategory { get; set; }
        public string PendingQty { get; set; }
        public string PendingDays { get; set; }
        public string itemcode { get; set; }
        //public string BranchName { get; set; }
        //public string download { get; set; }


    }
}