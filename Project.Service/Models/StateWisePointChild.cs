using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofStateWisePointchild
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public string Type { get; set; }

    }


    public class StateWisePointchilds
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<StateWisePointchild> data { get; set; }
    }

    public class StateWisePointchild
    {
        public string profileid { get; set; }
        public string FullName { get; set; }
        public string Categorynm { get; set; }
        public string PointType { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string BalancePoints { get; set; }
        public string ShopName { get; set; }


    }
}