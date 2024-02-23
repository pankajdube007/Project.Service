using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GetPointSchemeGStarList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string SchemeId { get; set; }
    }
    public class GetPointSchemeGStar
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetPointSchemeGStars> data { get; set; }
    }
    public class GetPointSchemeGStars
    {
        public string DisplayName { get; set; }

        public string SchemeId { get; set; }

        public string BranchName { get; set; }
        public string Cin { get; set; }
        public string TotalSale { get; set; } 
        public string BonustotalSale { get; set; } 
        public string Point { get; set; } 
        public string BonusPoint { get; set; } 
        public string TotalPoint { get; set; } 
        public string Gift { get; set; } 
        public string NextGift { get; set; } 
        public string GiftImage { get; set; } 
        public string NextGiftImage { get; set; } 
        public string PdfLink { get; set; }

        public string SchemeName { get; set; }


    }
}