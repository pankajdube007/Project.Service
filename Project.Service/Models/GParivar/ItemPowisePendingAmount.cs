using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    

    public class ListofitemPOwisepending
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int Potype { get; set; }
        [Required]
        public int itemid { get; set; }
        //[Required]
        //public int materialissuefrom { get; set; }
        



    }

    public class itemPOwisependingLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<itemPOwisependingList> data { get; set; }
    }

    public class itemPOwisependingList
    {
        public string Ponum { get; set; }
        public string SubCat { get; set; }
        public string ItemName { get; set; }
        public string Color { get; set; }
        public string Qty { get; set; }
        public string Amount { get; set; }
        public string DispatchFrom { get; set; }
        public string pendingsince { get; set; }



    }
}