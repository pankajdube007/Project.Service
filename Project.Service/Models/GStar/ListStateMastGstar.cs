using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{

    public class ListofStateMast
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class GetListStateMasts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListStateMast> data { get; set; }
    }

    public class GetListStateMast
    {
        public string SlNo { get; set; }
        public string statenm { get; set; }

    }
}