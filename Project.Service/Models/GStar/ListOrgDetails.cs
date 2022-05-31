using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
   
    public class ListOrgDetails
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int orgid { get; set; }

        [Required]
        public int orgcat { get; set; }
    }

    public class GetOrgDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetOrgDetailsList> data { get; set; }
    }

    public class GetOrgDetailsList
    {
        public string orgid { get; set; }
        public string compname { get; set; }
        public string orgcat { get; set; }
        public string partycatnm { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string regaddress { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
       
    }

}