using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{

    public class ListofMenInBlueSumParty
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    //public class GetMenInBlueSumPartyLists
    //{
    //    public bool result { get; set; }
    //    public string message { get; set; }
    //    public string servertime { get; set; }
    //    public List<GetMenInBlueSumPartyList> data { get; set; }
    //}

    public class GetMenInBlueSumPartyLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetFinalLists> data { get; set; }

    }

    public class GetFinalLists
    {
        public List<GetMenInBlueSumPartyList> partyList { get; set; }
        public List<GetMenInBlueSumDetailsList> detailList { get; set; }
    }

    public class GetMenInBlueSumPartyList
    {
        public string Name { get; set; }
        public string Partyid { get; set; }
        public string TypeCat { get; set; }
        public string TotalPoints { get; set; }
        public string DisplayName { get; set; }
        public string HomeBranch { get; set; }
        public string CurrentPrice { get; set; }
        public string CurrentPriceImg { get; set; }
        public string NextPrice { get; set; }
        public string NextPriceImg { get; set; }
        public string cin { get; set; }
        public string CurPricePoints { get; set; }
        public string NextPricePoints { get; set; }
        public string Totalsale { get; set; }
        public string meninbluefile { get; set; }
    }

    public class GetMenInBlueSumDetailsList
    {
        public string Partyid { get; set; }
        public string Division { get; set; }
        public string Slab { get; set; }
        public string Sale { get; set; }
        public string Point { get; set; }
        public string PartyName { get; set; }
        public string perpoint { get; set; }
        
    }
}