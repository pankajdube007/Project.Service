using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{

    public class ListofGstInformation
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int SlnoState { get; set; }

    }

    public class GetGstInformationLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetGstInformationList> data { get; set; }
    }

    public class GetGstInformationList
    {
        public string printnm { get; set; }
        public string FullAddress { get; set; }
        public string GSTNo { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
    }
}