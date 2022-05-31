using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class GetListStateMast
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }
    }

    public class GetListGetListStateMasts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListGetListStateMast> data { get; set; }
    }

    public class GetListGetListStateMast
    {
        public string SlNo { get; set; }
        public string statenm { get; set; }
        
    }

}