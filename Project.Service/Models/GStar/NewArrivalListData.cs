using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class NewArrivalListData
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
  

    public class NewArrivalAppLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NewArrivalAppList> data { get; set; }
    }

    public class NewArrivalAppList
    {
        public int Slno { get; set; }
        public string ProductCode { get; set; }

    }

}