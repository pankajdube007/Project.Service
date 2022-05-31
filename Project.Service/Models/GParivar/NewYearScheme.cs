using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Project.Service.Models
{
    public class NewYearScheme
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class SchemeForNewYear
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<SchemeData> data { get; set; }

    }

    public class SchemeData
    {
        //public string SchemeName { get; set; }
        public decimal TotalSale { get; set; }
        public int TotalEarnedPoint { get; set; }
        public int Currentslno { get; set; }
        public string CurrentSlab { get; set; }
        public string CurrentSlabPrice { get; set; }
        public string CurrentSlabimg { get; set; }
        public string CurrentSlabpoint { get; set; }
        public string NextSlab { get; set; }
        public string NextSlabPrice { get; set; }
        public string NextSlabimg { get; set; }
        public string NextSlabpoint { get; set; }
        public string fileview { get; set; }
        public string address { get; set; }
        public bool isselection { get; set; }
        public bool isEditable { get; set; }
        public List<DivisionWiseSale> division { get; set; }
    }

    public class DivisionWiseSale
    {
        public string Division { get; set; }
        public decimal Sales { get; set; }
        public int EarnedPoint { get; set; }
        public decimal OnePointPerSale { get; set; }

    }
}