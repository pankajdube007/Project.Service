using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   

    public class Listofvendorhighestsalespowise
    {
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [Required]
        public int ItemnId { get; set; }
        [Required]
        public int vendorid { get; set; }
    }
    public class vendorhighestsalespowiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<vendorhighestsalespowiseList> data { get; set; }
    }
    public class vendorhighestsalespowiseList
    {

        public string ItemCode { get; set; }
        public string Itemslno { get; set; }
        public string ItemDescription { get; set; }
        public string Subcategory { get; set; }
        public string Quantity { get; set; }
        public string BasicAmt { get; set; }
        public string FinalAmt { get; set; }
        public string ponum { get; set; }
        public string date { get; set; }


    }
}