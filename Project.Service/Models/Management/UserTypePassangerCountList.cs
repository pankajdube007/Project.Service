using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  

    public class ListofUserTypePassangerCount
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

    }

    public class UserTypePassangerCountLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<UserTypePassangerCountList> data { get; set; }
    }

    public class UserTypePassangerCountList
    {
        public string UserType { get; set; }
        public string PassangerCount { get; set; }
       
    }
}