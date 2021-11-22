using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
 
    public class ListofCatWisePending
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
        public int DivisionId { get; set; }



    }

    public class CatWisePendingLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CatWisePendingList> data { get; set; }
    }

    public class CatWisePendingList
    {
        public string DivisionId { get; set; }
      
        public string Division { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public string Amount { get; set; }
        public string Potype { get; set; }
        public string PartyId { get; set; }


    }
}