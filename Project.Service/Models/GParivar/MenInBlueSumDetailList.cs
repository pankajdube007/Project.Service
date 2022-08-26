using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
   
    public class ListofMenInBlueSumDetail
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetMenInBlueSumDetailLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetMenInBlueSumDetailList> data { get; set; }
    }

    public class GetMenInBlueSumDetailList
    {
        public string Partyid { get; set; }
        public string Division { get; set; }
        public string Slab { get; set; }
        public string Sale { get; set; }
        public string Point { get; set; }
        public string PartyName { get; set; }
        
    }
}