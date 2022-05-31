using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListOrgImg
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int orgid { get; set; }

        [Required]
        public int orgcat { get; set; }

        [Required]
        public int imgtype { get; set; }

        [Required]
        public string img { get; set; }

    }

    public class AddOrgImgLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddOrgImgList> data { get; set; }
    }

    public class AddOrgImgList
    {
        public string output { get; set; }
    }
}