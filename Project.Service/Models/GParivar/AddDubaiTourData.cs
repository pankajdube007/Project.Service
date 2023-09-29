using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class ListAddDubaiTourData
    {

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        public int GiftCount { get; set; }

        public string CnAmount { get; set; }

    }
    public class AddDubaiTourData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddDubaiTourDatas> data { get; set; }
    }
    public class AddDubaiTourDatas
    {
        public string output { get; set; }
    }
}