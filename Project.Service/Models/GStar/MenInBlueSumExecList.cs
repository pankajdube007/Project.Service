using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListofMenInBlueSumExec
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetMenInBlueSumExecLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetMenInBlueSumExecList> data { get; set; }
    }

    public class GetMenInBlueSumExecList
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
        
    }
}