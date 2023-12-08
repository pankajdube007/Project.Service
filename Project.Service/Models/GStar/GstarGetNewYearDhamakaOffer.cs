using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GstarGetNewYearDhamakaOfferList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class GstarGetNewYearDhamakaOffer
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GstarGetNewYearDhamakaOffers> data { get; set; }
    }
    public class GstarGetNewYearDhamakaOffers
    {
        public string CIN { get; set; }
        public string Name { get; set; }

        public string DivisionName { get; set; }

        public string Sale { get; set; }

        public string Point { get; set; }
        public string TotalPoint { get; set; }
        public string Bonus { get; set; }

        public string Reward { get; set; }

        public string NextReward { get; set; }

        public string Rewarding { get; set; }

        public string NextRewardImage { get; set; }

        public string PdfLink { get; set; }
    }
}