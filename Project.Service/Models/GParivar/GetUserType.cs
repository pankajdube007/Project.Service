using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class GetUserType
    {

        public class InputList
        {
            [Required] public string userid { get; set; }
            [Required] public string ClientSecret { get; set; }
        }

        public class GetResults
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public List<UserType> data { get; set; }
        }

        public class UserType
        {
           
            public string name { get; set; }
            public string userid { get; set; }
            public string usertype { get; set; }
        }
    }
}