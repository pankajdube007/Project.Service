using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   

    public class ListmerchantCatList
    {
        [Required]
        public int ExId { get; set; }


        [Required]
        public int slno { get; set; }
        
        
        public string Date { get; set; } 
        
        
        public int Trvlid { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetmerchantCatLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetmerchantCatList> data { get; set; }
    }

    public class GetmerchantCatList
    {
        public string SlNo { get; set; }
        public string name { get; set; }
        public string catimg { get; set; }
        public string usedamt { get; set; }
        public string Limit { get; set; }
        public string Balance { get; set; }
    }

}