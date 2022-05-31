using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class GetListDistrictMast
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int Stateid { get; set; }
    }

    public class GetListDistrictMasters
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListDistrictMaster> data { get; set; }
    }

    public class GetListDistrictMaster
    {
        public string SlNo { get; set; }
        public string Distrctnm { get; set; }

    }
}