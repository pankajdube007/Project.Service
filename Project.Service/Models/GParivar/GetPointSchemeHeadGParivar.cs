using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class GetPointSchemeHeadGParivarList
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string SchemeId { get; set; }
    }
    public class GetPointSchemeHeadGParivarLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetPointSchemeHeadGParivar> data { get; set; }
    }
    public class GetPointSchemeHeadGParivar
    {
        public string Gift { get; set; }
        public string NextGift { get; set; }
        public string GiftImg { get; set; }
        public string NextGiftImg { get; set; }
        public string PdfLink { get; set; }
        public string FinalPoint { get; set; }
        public string GiftPoint { get; set; }
        public string  NextGiftPoint { get; set; }
        public List<GetPointSchemeHeadGParivarListes> Details { get; set; }
    }
    public class GetPointSchemeHeadGParivarListes
    {
        public string DisplayName { get; set; }
        public string BranchName { get; set; }
        public string Division { get; set; }
        public string Cin { get; set; }
        public string Totalsale { get; set; }
        public string BonustotalSale { get; set; }
        public string Point { get; set; }
        public string BonusPoint { get; set; }
        public string TotalPoint { get; set; }
    }
}