using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    

    public class ListofTravelReq
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Date { get; set; }
        

    }

    public class GetTravelReqLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetTravelReqList> data { get; set; }
    }

    public class GetTravelReqList
    {
        public string slno { get; set; }
        public string refno { get; set; }
    }
}