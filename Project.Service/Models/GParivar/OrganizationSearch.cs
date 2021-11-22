using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
   

    public class ListsofOrganizationSearch
    {
        [Required]
        public int ExId { get; set; }

       
        public string searchtxt { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class OrganizationSearchs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OrganizationSearch> data { get; set; }
    }

    public class OrganizationSearch
    {
        public int orgid { get; set; }
        public string orgname { get; set; }
        public string catid { get; set; }
        public string catname { get; set; }
        public string areaid { get; set; }
        public string areaname { get; set; }
        public string orgaddress { get; set; }
    }
}