using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    
    public class submitGroupTODlist
    {
        [Required] public string CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int groupid { get; set; }
    }

    //public class submitGroupTODs
    //{
    //    public bool result { get; set; }
    //    public string message { get; set; }
    //    public string servertime { get; set; }
    //    public object data { get; set; }
    //}

}