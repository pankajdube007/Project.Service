using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Service.Models
{
    public class ItemListForProblem 
    {
   
        [Required]
        public string ClientSecret { get; set; }
    }

    public class GetItemList
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ItemList> data { get; set; }
    }

    public class ItemList
    {

        public int slno { get; set; }
        public string Item { get; set; }
        
    }


}