using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class Listofitemwisepending
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
        public int Catid { get; set; }



    }

    public class itemwisependingLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<itemwisependingList> data { get; set; }
    }

    public class itemwisependingList
    {
        public string SubCatId { get; set; }
        public string SubCat { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Color { get; set; }
        public string Qty { get; set; }
        public string Amount { get; set; }
        //public string DispatchFrom { get; set; }
        public string pendingsince { get; set; }
        public string Branch { get; set; }
        public string Potype { get; set; }
        //public string materialissuefrom { get; set; }


    }
}

