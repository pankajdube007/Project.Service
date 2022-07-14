using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    
    public class ListofDeActivateAccount
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string loginid { get; set; }

        [Required]
        public int appid { get; set; }
    }

    public class GetDeActivateAccountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetDeActivateAccountList> data { get; set; }
    }

    public class GetDeActivateAccountList
    {
        public string msg { get; set; }
       
    }
}