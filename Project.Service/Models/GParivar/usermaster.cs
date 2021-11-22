using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class usermaster
    {
        [Required]
        public string emailid { get; set; }
    }

    public class usermasters
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<userdetails> data { get; set; }
    }

    public class userdetails
    {

        public int slno { get; set; }
        public string name { get; set; }
        public string usernm { get; set; }
        public string password { get; set; }
    }


}